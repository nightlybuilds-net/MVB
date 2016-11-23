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
        /// Remove All Action
        /// </summary>
        void Clear();

        /// <summary>
        /// Remove all action for a subscriber
        /// </summary>
        /// <param name="subscriber"></param>
        void Clear(object subscriber);

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
        /// Remove All Action
        /// </summary>
        void Clear();

        /// <summary>
        /// Remove all action for a subscriber
        /// </summary>
        /// <param name="subscriber"></param>
        void Clear(object subscriber);

        /// <summary>
        /// Invoke all actions
        /// </summary>
        void Invoke(T arg);
    }

   
}