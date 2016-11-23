using System;

namespace Mvb.Core.Components.BinderActions
{
    public interface IBinderActions
    {
        /// <summary>
        /// Add Action 
        /// </summary>
        /// <param name="subscriber"></param>
        /// <param name="action"></param>
        void AddAction(object subscriber,Action action);

        /// <summary>
        /// Invoke all actions
        /// </summary>
        void Invoke();
    }

    public interface IBinderActions<T>
    {
        /// <summary>
        /// Add Action 
        /// </summary>
        /// <param name="subscriber"></param>
        /// <param name="action"></param>
        void AddAction(object subscriber,Action<T> action);

        /// <summary>
        /// Invoke all actions
        /// </summary>
        void Invoke(T arg);
    }

   
}