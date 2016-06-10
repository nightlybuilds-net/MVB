using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Mvb.Cross.Abstract;

namespace Mvb.Cross
{
    public class MvBinder
    {
        private readonly MvbBase _vmInstance;

        private readonly IUiRunner _uiRunner;
        private readonly Dictionary<string, ICollection<Action>> _runDictionary;
        private readonly Dictionary<string, ICollection<Action<NotifyCollectionChangedEventArgs>>> _runCollectionDictionary;

        public MvBinder(MvbBase vmInstance)
        {
            this._vmInstance = vmInstance;
            this._runDictionary = new Dictionary<string, ICollection<Action>>();
            this._runCollectionDictionary = new Dictionary<string, ICollection<Action<NotifyCollectionChangedEventArgs>>>();

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
                this._runDictionary.Add(id, new List<Action> { action });
        }

        public void AddAction<TSource>(Expression<Func<TSource, object>> property, Action action)
        {
            var propName = this.GetPropertyName(property);
            this.AddAction(propName,action);
        }

        public void AddActionForCollection<TSource>(Expression<Func<TSource, object>> property, Action<NotifyCollectionChangedEventArgs> action)
        {
            var propName = this.GetPropertyName(property);

            this.AddActionForCollection(propName, action);
        }

        public void AddActionForCollection(string propertyName, Action<NotifyCollectionChangedEventArgs> action)
        {
            //add to list
            if (this._runCollectionDictionary.ContainsKey(propertyName))
                this._runCollectionDictionary[propertyName].Add(action);
            else
                this._runCollectionDictionary.Add(propertyName, new List<Action<NotifyCollectionChangedEventArgs>> { action });
        }

        /// <summary>
        /// Run all action for that property
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="property"></param>
        public void Run<TSource>(Expression<Func<TSource, object>> property)
        {
            var propName = this.GetPropertyName(property);
            this.Run(propName);
        }

        /// <summary>
        /// Run all action for that property
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
        /// Run all action for collection changed
        /// </summary>
        /// <param name="property"></param>
        /// <param name="args"></param>
        public void RunCollection<TSource>(Expression<Func<TSource, object>> property, NotifyCollectionChangedEventArgs args)
        {
            var propName = this.GetPropertyName(property);
            this.RunCollection(propName,args);
        }

        /// <summary>
        /// Run all action for collection changed
        /// </summary>
        /// <param name="property"></param>
        /// <param name="args"></param>
        public void RunCollection(string property, NotifyCollectionChangedEventArgs args)
        {
            ICollection<Action<NotifyCollectionChangedEventArgs>> value;
            if (!this._runCollectionDictionary.TryGetValue(property, out value)) return;

            foreach (var action in value)
                this._uiRunner.Run(action,args);
        }

        #region PRIVATE
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

        private void ActiveListenerOnObservableCollection(object obj)
        {
            var typeInfo = obj.GetType().GetTypeInfo();

            foreach (var info in typeInfo.DeclaredProperties)
            {

                var isObservable = info.PropertyType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(INotifyCollectionChanged));

                if (!isObservable) continue;

                var obserableProp = (INotifyCollectionChanged)info.GetValue(obj, null);

                obserableProp.CollectionChanged += (sender, args) =>
                {
                    this.RunCollection(info.Name, args);
                };
            }

        }


        private void ActiveListener()
        {
            //Subscribe
            this._vmInstance.PropertyChanged += (sender, args) =>
            {
                this.Run(args.PropertyName);
            };
            this.ActiveListenerOnObservableCollection(this._vmInstance);
        } 
        #endregion
    }
}