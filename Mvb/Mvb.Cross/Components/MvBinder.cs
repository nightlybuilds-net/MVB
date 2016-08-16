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
        private readonly Dictionary<string, ICollection<Action<MvbCollectionUpdateArgs>>> _runCollectionDictionary;
        private readonly Dictionary<string, ICollection<Action>> _runDictionary;

        private readonly IUiRunner _uiRunner;
        private readonly MvbBase _vmInstance;

        public MvBinder(MvbBase vmInstance)
        {
            this._vmInstance = vmInstance;
            this._runDictionary = new Dictionary<string, ICollection<Action>>();
            this._runCollectionDictionary = new Dictionary<string, ICollection<Action<MvbCollectionUpdateArgs>>>();

            //On UiThread Runner
            this._uiRunner = UiRunnerDispenser.GetRunner();

            //Active listener on VM
            this.ActiveListener();
        }

        public void AddAction(string id, Action action)
        {
            //add to list
            if (this._runDictionary.ContainsKey(id))
                this._runDictionary[id].Add(action);
            else
                this._runDictionary.Add(id, new List<Action> {action});
        }

        public void AddAction<TSource>(Expression<Func<TSource, object>> property, Action action)
        {
            var propName = this.GetPropertyName(property);
            this.AddAction(propName, action);
        }

        public void AddActionForCollection<TSource>(Expression<Func<TSource, object>> property,
            Action<MvbCollectionUpdateArgs> action)
        {
            var propName = this.GetPropertyName(property);

            this.AddActionForCollection(propName, action);
        }

        public void AddActionForCollection(string propertyName, Action<MvbCollectionUpdateArgs> action)
        {
            //add to list
            if (this._runCollectionDictionary.ContainsKey(propertyName))
                this._runCollectionDictionary[propertyName].Add(action);
            else
                this._runCollectionDictionary.Add(propertyName, new List<Action<MvbCollectionUpdateArgs>> {action});
        }

        /// <summary>
        ///     Run all action for that property
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="property"></param>
        public void Run<TSource>(Expression<Func<TSource, object>> property)
        {
            var propName = this.GetPropertyName(property);
            this.Run(propName);
        }

        /// <summary>
        ///     Run all action for that property
        /// </summary>
        /// <param name="property"></param>
        public void Run(string property)
        {
            ICollection<Action> value;
            if (!this._runDictionary.TryGetValue(property, out value)) return;

            foreach (var action in value)
                this._uiRunner.Run(action);
        }

        /// <summary>
        ///     Run all action for collection changed
        /// </summary>
        /// <param name="property"></param>
        /// <param name="args"></param>
        public void RunCollection<TSource>(Expression<Func<TSource, object>> property, MvbCollectionUpdateArgs args)
        {
            var propName = this.GetPropertyName(property);
            this.RunCollection(propName, args);
        }

        /// <summary>
        ///     Run all action for collection changed
        /// </summary>
        /// <param name="property"></param>
        /// <param name="args"></param>
        public void RunCollection(string property, MvbCollectionUpdateArgs args)
        {
            ICollection<Action<MvbCollectionUpdateArgs>> value;
            if (!this._runCollectionDictionary.TryGetValue(property, out value)) return;

            foreach (var action in value)
                this._uiRunner.Run(action, args);
        }

        #region PRIVATE

        private string GetPropertyName<T>(Expression<Func<T, object>> property)
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
                memberExpression = (MemberExpression) lambda.Body;
            }

            return ((PropertyInfo) memberExpression.Member).Name;
        }

        private void ActiveListenerOnObservableCollection(object obj)
        {
            var typeInfo = obj.GetType().GetTypeInfo();

            foreach (var info in typeInfo.DeclaredProperties)
            {
                var isMvbCollection =
                    info.PropertyType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IMvbCollection));

                if (!isMvbCollection) continue;

                var obserableProp = (IMvbCollection) info.GetValue(obj, null);

                obserableProp.CollectionChanged += (sender, args) =>
                {
                    var mvbArgs = new MvbCollectionUpdateArgs
                    {
                        MvbUpdateAction = MvbUpdateAction.CollectionChanged,
                        NotifyCollectionChangedEventArgs = args
                    };
                    this.RunCollection(info.Name, mvbArgs);
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
            this._vmInstance.PropertyChanged += (sender, args) => { this.Run(args.PropertyName); };
            this.ActiveListenerOnObservableCollection(this._vmInstance);
        }

        #endregion
    }
}