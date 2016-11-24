using System;
using Mvb.Core.Abstract;
using Mvb.Core.Components;

namespace Mvb.Core.Base
{
    public abstract class MvbBase : MvbBindable, IMvbMessenger
    {
        private static readonly IMvbMessenger Messenger = new MvbMessenger(); 

        public MvBinder Binder { get; private set; }

        protected void InitBinder()
        {
            this.Binder = new MvBinder(this);
        }

        #region MESSENGER
        public void Send<TSender, TArgs>(TSender sender, string message, TArgs args) where TSender : class
        {
            Messenger.Send(sender, message, args);
        }

        public void Send<TSender>(TSender sender, string message) where TSender : class
        {
            Messenger.Send(sender, message);
        }

        public void Subscribe<TSender, TArgs>(object subscriber, string message, Action<TSender, TArgs> callback, TSender source = default(TSender)) where TSender : class
        {
            Messenger.Subscribe(subscriber, message, callback, source);
        }

        public void Subscribe<TSender>(object subscriber, string message, Action<TSender> callback, TSender source = default(TSender)) where TSender : class
        {
            Messenger.Subscribe(subscriber, message, callback, source);
        }

        public void Unsubscribe<TSender, TArgs>(object subscriber, string message) where TSender : class
        {
            Messenger.Unsubscribe<TSender, TArgs>(subscriber, message);
        }

        public void Unsubscribe<TSender>(object subscriber, string message) where TSender : class
        {
            Messenger.Unsubscribe<TSender>(subscriber, message);
        }

        public void ResetMessenger()
        {
            Messenger.ResetMessenger();
        } 
        #endregion
    }
}
