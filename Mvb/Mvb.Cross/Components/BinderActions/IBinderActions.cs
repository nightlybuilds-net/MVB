using System;

namespace Mvb.Core.Components.BinderActions
{
    public interface IBinderActions
    {
        /// <summary>
        /// Add Action 
        /// </summary>
        /// <param name="action"></param>
        void Add(Action action);

        /// <summary>
        /// Invoke all actions
        /// </summary>
        void Invoke();
    }
}