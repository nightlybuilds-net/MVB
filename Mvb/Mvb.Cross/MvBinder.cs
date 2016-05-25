using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Mvb.Cross.Abstract;
using RemIoc;

namespace Mvb.Cross
{
    public class MvBinder<T> where T: MvbBase
    {
        //MVB Instance
        public T VmInstance { get; }

        private readonly IUiRunner _uiRunner;
        private readonly Dictionary<string, ICollection<Action>> _runDictionary;

        public MvBinder(T vmInstance)
        {
            this._runDictionary = new Dictionary<string, ICollection<Action>>();
            this.VmInstance = vmInstance;

            //On UiThread Runner
            this._uiRunner = RIoc.Resolve<IUiRunner>();

            //Active listener on VM
            this.ActiveListener();
        }

        private void ActiveListener()
        {
            //Subscribe
            this.VmInstance.PropertyChanged += (sender, args) =>
            {
                Debug.WriteLine($"Property changed: {args.PropertyName}");
                this.Run(args.PropertyName);
            };
            this.ActiveListenerOnObservableCollection(this.VmInstance);
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

            foreach (PropertyInfo info in typeInfo.DeclaredProperties)
            {

                var isObservable = info.PropertyType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(INotifyCollectionChanged));

                if(!isObservable) continue;

                var obserableProp = (INotifyCollectionChanged)info.GetValue(obj, null);

                obserableProp.CollectionChanged += (sender, args) =>
                {
                    Debug.WriteLine("Sender: " + sender);
                };
            }

        }
    }
}