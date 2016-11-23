using System;
using System.Collections.Generic;
using System.Linq;
using Mvb.Core.Abstract;

namespace Mvb.Core.Components.BinderActions
{
    public class MvbActions : IBinderActions
    {
        private readonly IUiRunner _uiRunner;
        private readonly IList<Tuple<WeakReference,Action>> _bindersAction;

        public MvbActions()
        {
            //On UiThread Runner
            this._uiRunner = UiRunnerDispenser.GetRunner();
            this._bindersAction = new List<Tuple<WeakReference, Action>>();
        }

        public void AddAction(object subscriber, Action action)
        {
            this._bindersAction.Add(new Tuple<WeakReference, Action>(new WeakReference(subscriber),action));
        }

        public void Clear()
        {
            this._bindersAction.Clear();
        }

        public void Clear(object subscriber)
        {
            var toRemove = this._bindersAction.Where(w => w.Item1.Target.Equals(subscriber)).ToList();

            foreach (var tuple in toRemove)
                this._bindersAction.Remove(tuple);
        }

        public void Invoke()
        {
            var toRemove = new List<Tuple<WeakReference, Action>>();
            foreach (var tuple in this._bindersAction)
            {
                if(tuple.Item1.IsAlive)
                    this._uiRunner.Run(tuple.Item2);
                else
                    toRemove.Add(tuple);
            }

            // clear dead object actions
            foreach (var tuple in toRemove)
                this._bindersAction.Remove(tuple);
        }
    }

   

    public class MvbActions<T> : IBinderActions<T>
    {
        private readonly IUiRunner _uiRunner;
        private readonly IList<Tuple<WeakReference,Action<T>>> _bindersAction;


        public MvbActions()
        {
            //On UiThread Runner
            this._uiRunner = UiRunnerDispenser.GetRunner();
            this._bindersAction = new List<Tuple<WeakReference, Action<T>>>();
        }

      
        public void AddAction(object subscriber, Action<T> action)
        {
            this._bindersAction.Add(new Tuple<WeakReference, Action<T>>(new WeakReference(subscriber),action));
        }

        public void Clear()
        {
            this._bindersAction.Clear();
        }

        public void Clear(object subscriber)
        {
            var toRemove = this._bindersAction.Where(w => w.Item1.Target.Equals(subscriber)).ToList();

            foreach (var tuple in toRemove)
                this._bindersAction.Remove(tuple);
        }

        public void Invoke(T arg)
        {
            var toRemove = new List<Tuple<WeakReference, Action<T>>>();
            foreach (var tuple in this._bindersAction)
            {
                if (tuple.Item1.IsAlive)
                    this._uiRunner.Run(tuple.Item2,arg);
                else
                    toRemove.Add(tuple);
            }

            // clear dead object actions
            foreach (var tuple in toRemove)
                this._bindersAction.Remove(tuple);
        }
    }
}