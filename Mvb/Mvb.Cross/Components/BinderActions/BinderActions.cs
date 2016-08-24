using System;
using System.Collections.Generic;
using Mvb.Core.Abstract;

namespace Mvb.Core.Components.BinderActions
{
    public class BinderActions : IBinderActions
    {
        private readonly IUiRunner _uiRunner;
        private readonly IList<Action> _bindersAction;

        public BinderActions()
        {
            //On UiThread Runner
            this._uiRunner = UiRunnerDispenser.GetRunner();
            this._bindersAction = new List<Action>();
        }

        public void Add(Action action)
        {
            this._bindersAction.Add(action);
        }

        public void Invoke()
        {
            foreach (var action in this._bindersAction)
                this._uiRunner.Run(action);
        }
    }

    public class BinderActions<T>
    {
        private readonly IUiRunner _uiRunner;
        private readonly IList<Action<T>> _bindersAction;

        public BinderActions()
        {
            //On UiThread Runner
            this._uiRunner = UiRunnerDispenser.GetRunner();
            this._bindersAction = new List<Action<T>>();
        }

        public void Add(Action<T> action)
        {
            this._bindersAction.Add(action);
        }

        public void Invoke(T arg)
        {
            foreach (var action in this._bindersAction)
                this._uiRunner.Run(action, arg);
        }
    }
}