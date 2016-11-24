using System;
using System.Collections.Generic;
using System.Linq;
using Mvb.Core.Abstract;

namespace Mvb.Core.Components.BinderActions
{
    public class MvbActions : IBinderActions
    {
        private readonly IUiRunner _uiRunner;
        private readonly IList<Tuple<WeakReference,WeakReference<Action>>> _bindersAction;

        public MvbActions()
        {
            //On UiThread Runner
            this._uiRunner = Dispenser.GetRunner();
            this._bindersAction = new List<Tuple<WeakReference, WeakReference<Action>>>();
        }

        public void AddAction(object subscriber, Action action)
        {
            this._bindersAction.Add(new Tuple<WeakReference, WeakReference<Action>>(
                new WeakReference(subscriber), new WeakReference<Action>(action)));
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
            var toRemove = new List<Tuple<WeakReference, WeakReference<Action>>>();
            foreach (var tuple in this._bindersAction)
            {
                Action action;
                var extracted = tuple.Item2.TryGetTarget(out action);

                if(tuple.Item1.IsAlive && extracted)
                    this._uiRunner.Run(action);
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
        private readonly IList<Tuple<WeakReference,WeakReference<Action<T>>>> _bindersAction;


        public MvbActions()
        {
            //On UiThread Runner
            this._uiRunner = Dispenser.GetRunner();
            this._bindersAction = new List<Tuple<WeakReference, WeakReference<Action<T>>>>();
        }

      
        public void AddAction(object subscriber, Action<T> action)
        {
            this._bindersAction.Add(new Tuple<WeakReference, WeakReference<Action<T>>>(new WeakReference(subscriber),new WeakReference<Action<T>>(action)));
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
            var toRemove = new List<Tuple<WeakReference, WeakReference<Action<T>>>>();
            foreach (var tuple in this._bindersAction)
            {
                Action<T> action;
                var extracted = tuple.Item2.TryGetTarget(out action);

                if (tuple.Item1.IsAlive && extracted)
                    this._uiRunner.Run(action,arg);
                else
                    toRemove.Add(tuple);
            }

            // clear dead object actions
            foreach (var tuple in toRemove)
                this._bindersAction.Remove(tuple);
        }
    }
}