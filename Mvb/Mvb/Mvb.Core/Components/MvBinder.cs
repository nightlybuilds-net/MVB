using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Mvb.Core.Abstract;
using Mvb.Core.Args;
using Mvb.Core.Base;

namespace Mvb.Core.Components
{
    public class MvBinder
    {
        private readonly Dictionary<string, ICollection<Tuple<WeakReference, Action<MvbCollectionUpdateArgs>>>> _runCollectionDictionary;
        private readonly Dictionary<string, ICollection<Tuple<WeakReference,Action>>> _runDictionary;

        private readonly IUiRunner _uiRunner;
        private readonly MvbBase _vmInstance;

        public MvBinder(MvbBase vmInstance)
        {
            this._vmInstance = vmInstance;
            this._runDictionary = new Dictionary<string, ICollection<Tuple<WeakReference, Action>>>();
            this._runCollectionDictionary = new Dictionary<string, ICollection<Tuple<WeakReference, Action<MvbCollectionUpdateArgs>>>>();

            //On UiThread Runner
            this._uiRunner = UiRunnerDispenser.GetRunner();

            //Active listener on VM
            this.ActiveListener();
        }

        /// <summary>
        /// Add Action for property
        /// </summary>
        /// <param name="id">property name</param>
        /// <param name="action">action</param>
        /// <param name="subscriber">subscriber</param>
        public void AddAction(object subscriber,string id, Action action)
        {
            //add to list
            var weakref = new WeakReference(subscriber);

            if (this._runDictionary.ContainsKey(id))
                this._runDictionary[id].Add(new Tuple<WeakReference, Action>(weakref, action));
            else
                this._runDictionary.Add(id, new List<Tuple<WeakReference, Action>>() {new Tuple<WeakReference, Action>(weakref,action)});
        }

        /// <summary>
        /// Add action for property
        /// </summary>
        /// <typeparam name="TSource">TSource</typeparam>
        /// <param name="subscriber">subscriber</param>
        /// <param name="property">property</param>
        /// <param name="action">action</param>
        public void AddAction<TSource>(object subscriber,Expression<Func<TSource, object>> property, Action action ) where TSource : MvbBase
        {
            var propName = this.GetCompositePropertyName(property);
            this.AddAction(subscriber,propName, action);
        }

        /// <summary>
        /// Add action for collection property
        /// </summary>
        /// <typeparam name="TSource">TSource IMvbCollection</typeparam>
        /// <param name="subscriber"></param>
        /// <param name="property"></param>
        /// <param name="action"></param>
        public void AddActionForCollection<TSource>(object subscriber, Expression<Func<TSource, object>> property,
            Action<MvbCollectionUpdateArgs> action) where TSource : MvbBase
        {
            var propName = this.GetCompositePropertyName(property);

            this.AddActionForCollection(subscriber,propName, action);
        }

        /// <summary>
        /// Add action for collection property
        /// </summary>
        /// <param name="subscriber">subscriber object</param>
        /// <param name="propertyName"></param>
        /// <param name="action"></param>
        public void AddActionForCollection(object subscriber,string propertyName, Action<MvbCollectionUpdateArgs> action)
        {
            var weakref = new WeakReference(subscriber);

            //add to list
            if (this._runCollectionDictionary.ContainsKey(propertyName))
                this._runCollectionDictionary[propertyName].Add(new Tuple<WeakReference, Action<MvbCollectionUpdateArgs>>(weakref, action));
            else
                this._runCollectionDictionary.Add(propertyName, new List<Tuple<WeakReference, Action<MvbCollectionUpdateArgs>>>() { new Tuple<WeakReference, Action<MvbCollectionUpdateArgs>>(weakref, action) });
        }

        /// <summary>
        ///     Run all action for that property
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="property"></param>
        public void Run<TSource>(Expression<Func<TSource, object>> property) where TSource : MvbBase
        {
            var propName = this.GetCompositePropertyName(property);
            this.Run(propName);
        }

        /// <summary>
        ///     Run all action for that property
        /// </summary>
        /// <param name="property"></param>
        public void Run(string property)
        {
            ICollection<Tuple<WeakReference,Action>> value;
            if (!this._runDictionary.TryGetValue(property, out value)) return;

            // Run every action on live subscriber
            foreach (var tuple in value)
            {
                if (tuple.Item1.IsAlive)
                    this._uiRunner.Run(tuple.Item2);
                else
                    this._runDictionary[property].Remove(tuple);
            }
        }

        /// <summary>
        ///     Run all action for collection changed
        /// </summary>
        /// <param name="property"></param>
        /// <param name="args"></param>
        public void RunCollection<TSource>(Expression<Func<TSource, object>> property, MvbCollectionUpdateArgs args) where TSource : MvbBase
        {
            var propName = this.GetCompositePropertyName(property);
            this.RunCollection(propName, args);
        }

        /// <summary>
        ///     Run all action for collection changed
        /// </summary>
        /// <param name="property"></param>
        /// <param name="args"></param>
        public void RunCollection(string property, MvbCollectionUpdateArgs args)
        {
            ICollection<Tuple<WeakReference,Action<MvbCollectionUpdateArgs>>> value;
            if (!this._runCollectionDictionary.TryGetValue(property, out value)) return;

            foreach (var tuple in value)
            {
                if (tuple.Item1.IsAlive)
                    this._uiRunner.Run(tuple.Item2,args);
                else
                    this._runCollectionDictionary[property].Remove(tuple);
            }
        }

        /// <summary>
        /// Remove All actions
        /// </summary>
        public void ClearActions()
        {
            this._runDictionary.Clear();
        }

        /// <summary>
        /// Create istance of MvbBindable on this Binder
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TK"></typeparam>
        /// <param name="property"></param>
        /// <param name="newIstance"></param>
        public void SetMvbBindableInstance<T,TK>(Expression<Func<T, TK>> property, Func<TK> newIstance) where T : MvbBase where TK : IMvbBindable
        {
            var bindableInstance =  newIstance.Invoke();

            var propertyTree = new Queue<string>();

            #region PROPERTY TREE

            LambdaExpression lambda = property;
            MemberExpression memberExpression;

            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)lambda.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            //Check composite ex binder.MyObject.MyProperty => MyObject.MyProperty
            var propName = ((PropertyInfo)memberExpression.Member).Name;
            var body = lambda.Body.ToString();
            var bodySplitted = body.Split('.');
            var isComposite = bodySplitted.Length > 2;

            if (!isComposite)
                propertyTree.Enqueue(propName);
            else
            {
                var splittedlist = bodySplitted.ToList();
                splittedlist.RemoveAt(0);
                foreach (var s in splittedlist)
                    propertyTree.Enqueue(s);
            }
            var theDeepProperty = string.Join(".", propertyTree);

            #endregion

            var vmtype = this._vmInstance.GetType();
            if(vmtype != typeof(T))
                throw new Exception($"Wrong type instance. instance must be of type: {vmtype}");

            PropertyInfo propertyOnObj = null;
            object lastParent = this._vmInstance;

            while (propertyTree.Any())
            {
                var propertyName = propertyTree.Dequeue();
                var parentTypeInfo = lastParent.GetType().GetTypeInfo();

                propertyOnObj = parentTypeInfo.DeclaredProperties.Single(s => s.Name == propertyName);

                if(propertyTree.Count > 0)
                    lastParent = propertyOnObj.GetValue(lastParent);
            }


            if(propertyOnObj == null)
                throw new Exception("Deep property not found!");

            var oldObj = propertyOnObj.GetValue(lastParent, null) as IMvbBindable;


            // Clear and Active PropertyChanged
            //Clear ols handler
            oldObj?.ClearHandler();

            //set new object
            propertyOnObj.SetValue(lastParent, bindableInstance);

            //Active PropertyChanged if not null
            if (bindableInstance == null) return;
           
            bindableInstance.PropertyChanged += (sender, args) =>
            {
                var registerName = $"{theDeepProperty}.{args.PropertyName}";
                this.Run(registerName);
            };

            //Activate sub collections
            this.ActiveListenerOnObservableCollection(bindableInstance, propertyOnObj.Name);
        }

        #region PRIVATE
        /// <summary>
        /// TODO Clear unused
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        private string GetPropertyName<T>(Expression<Func<T, object>> property)
        {
            LambdaExpression lambda = property;
            MemberExpression memberExpression;

            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)lambda.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            return ((PropertyInfo)memberExpression.Member).Name;
        }

        private string GetCompositePropertyName<T>(Expression<Func<T, object>> property)
        {
            LambdaExpression lambda = property;
            MemberExpression memberExpression;

            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression) lambda.Body;
                memberExpression = (MemberExpression) unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            //Check composite ex binder.MyObject.MyProperty => MyObject.MyProperty
            var propName = ((PropertyInfo)memberExpression.Member).Name;
            var body = lambda.Body.ToString();
            var bodySplitted = body.Split('.');
            var isComposite = bodySplitted.Length > 2;

            if (!isComposite) return propName;

            var splittedlist = bodySplitted.ToList();
            splittedlist.RemoveAt(0);
            splittedlist.RemoveAt(splittedlist.Count-1);

            return $"{string.Join(".",splittedlist)}.{propName}";
        }
        
        private void ActiveListenerOnObservableCollection(object obj, string propertyNameSuffix = null)
        {
            var typeInfo = obj.GetType().GetTypeInfo();

            foreach (var info in typeInfo.DeclaredProperties)
            {
                var isMvbCollection =
                    info.PropertyType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IMvbCollection));

                if (!isMvbCollection) continue;

                var obserableProp = (IMvbCollection) info.GetValue(obj, null);
                if(obserableProp == null) continue;

                var runPropertyName = propertyNameSuffix != null ? $"{propertyNameSuffix}.{info.Name}" : info.Name;
                runPropertyName = runPropertyName.TrimStart('.');
                obserableProp.CollectionChanged += (sender, args) =>
                {
                    var mvbArgs = new MvbCollectionUpdateArgs
                    {
                        MvbUpdateAction = MvbUpdateAction.CollectionChanged,
                        NotifyCollectionChangedEventArgs = args
                    };
                    this.RunCollection(runPropertyName, mvbArgs);
                };

                obserableProp.MvbItemCollectionChanged += (sender, args) =>
                {
                    var mvbArgs = new MvbCollectionUpdateArgs
                    {
                        MvbUpdateAction = MvbUpdateAction.ItemChanged,
                        MvbCollectionItemChanged = args
                    };
                    this.RunCollection(info.Name, mvbArgs);
                };
            }
        }
        
        private void ActiveListener()
        {
            //Subscribe
            this.RecursivelyActiveListener(this._vmInstance);
        }

        private void RecursivelyActiveListener(object obj, string parent = "")
        {
            // Active on main object
            var mainBindable = obj as MvbBindable;
            if (mainBindable != null)
            {
                mainBindable.PropertyChanged += (sender, args) =>
                {
                    var registerName = string.IsNullOrWhiteSpace(parent) ? $"{args.PropertyName}" : $"{parent}.{args.PropertyName}";
                    this.Run(registerName);
                };
            }

            this.ActiveListenerOnObservableCollection(obj, parent);

            var typeInfo = obj.GetType().GetTypeInfo();

            foreach (var info in typeInfo.DeclaredProperties)
            {
                var obserableProp = info.GetValue(obj, null) as MvbBindable;
                if (obserableProp == null) continue;

                var register = $"{parent}.{info.Name}";
                register = register.TrimStart('.');
                this.RecursivelyActiveListener(obserableProp, register);
            }
        }

        #endregion
    }
}