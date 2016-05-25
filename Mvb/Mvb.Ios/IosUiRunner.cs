using System;
using System.Collections.Specialized;
using Mvb.Cross.Abstract;
using UIKit;

namespace Mvb.Ios
{
    public class IosUiRunner : IUiRunner
    {
        public void Run(Action action)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(action);
        }

        public void Run(Action<NotifyCollectionChangedEventArgs> action, NotifyCollectionChangedEventArgs args)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(()=> {action.Invoke(args);});
        }
    }
}
