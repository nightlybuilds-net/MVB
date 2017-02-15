using System;

namespace Mvb.Core.Components.BinderActions
{
	public interface IBinderActions : IClearable
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

    public interface IBinderActions<T> : IClearable
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


	public interface IClearable
	{ 
		  /// <summary>
        /// Remove All Action
        /// </summary>
        void Clear();

		/// <summary>
		/// Remove all action for a subscriber
		/// </summary>
		/// <param name="subscriber"></param>
		void Clear(object subscriber);
	}
   
}