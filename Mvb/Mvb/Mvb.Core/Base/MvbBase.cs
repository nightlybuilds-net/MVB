using System;
using System.Linq;
using System.Reflection;
using Mvb.Core.Abstract;
using Mvb.Core.Components;
using Mvb.Core.Components.BinderActions;

namespace Mvb.Core.Base
{
    public abstract class MvbBase : MvbBindable, IMvbMessenger
    {

        public MvBinder Binder { get; private set; }

        protected void InitBinder()
        {
            this.Binder = new MvBinder(this);
        }

		#region CLEAR BINDER
		/// <summary>
		/// Clears all subscribers from binder.
		/// Clear all declared mvbaction too
		/// </summary>
		public void ClearAll()
		{
			// clear actions on binder
			this.Binder?.ClearActions();

			// clear actions on declared mvbaction
			var typeInfo = this.GetType().GetTypeInfo();

			foreach (var info in typeInfo.DeclaredProperties)
			{
				var isClearable =
					info.PropertyType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IClearable));

				if (!isClearable) continue;

				var mvbActions = (IClearable)info.GetValue(this, null);
				mvbActions.Clear();
			}
		}


		/// <summary>
		/// Clears passed subscriber from binder.
		/// Clear all declared mvbaction too
		/// </summary>
		/// <param name="subscriber">Subscriber.</param>
		public void ClearAll(object subscriber)
		{
			// clear actions on binder
			this.Binder?.ClearActions(subscriber);

			// clear actions on declared mvbaction
			var typeInfo = this.GetType().GetTypeInfo();

			foreach (var info in typeInfo.DeclaredProperties)
			{
				var isClearable =
					info.PropertyType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IClearable));

				if (!isClearable) continue;

				var mvbActions = (IClearable)info.GetValue(this, null);
				mvbActions.Clear(subscriber);
			}
		}


		#endregion

        #region MESSENGER
        public void Send<TSender, TArgs>(TSender sender, string message, TArgs args) where TSender : class
        {
            Dispenser.GetMessenger().Send(sender, message, args);
        }

        public void Send<TSender>(TSender sender, string message) where TSender : class
        {
            Dispenser.GetMessenger().Send(sender, message);
        }

        public void Subscribe<TSender, TArgs>(object subscriber, string message, Action<TSender, TArgs> callback, TSender source = default(TSender)) where TSender : class
        {
            Dispenser.GetMessenger().Subscribe(subscriber, message, callback, source);
        }

        public void Subscribe<TSender>(object subscriber, string message, Action<TSender> callback, TSender source = default(TSender)) where TSender : class
        {
            Dispenser.GetMessenger().Subscribe(subscriber, message, callback, source);
        }

        public void Unsubscribe<TSender, TArgs>(object subscriber, string message) where TSender : class
        {
            Dispenser.GetMessenger().Unsubscribe<TSender, TArgs>(subscriber, message);
        }

        public void Unsubscribe<TSender>(object subscriber, string message) where TSender : class
        {
            Dispenser.GetMessenger().Unsubscribe<TSender>(subscriber, message);
        }

        public void ResetMessenger()
        {
            Dispenser.GetMessenger().ResetMessenger();
        } 
        #endregion
    }
}
