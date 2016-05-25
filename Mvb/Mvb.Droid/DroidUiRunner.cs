using System;
using System.Collections.Specialized;
using Mvb.Cross.Abstract;
using Plugin.CurrentActivity;

namespace Mvb.Droid
{
    class DroidUiRunner : IUiRunner
    {
        public void Run(Action action)
        {
            CrossCurrentActivity.Current.Activity.RunOnUiThread(action.Invoke);
        }

        public void Run(Action<NotifyCollectionChangedEventArgs> action, NotifyCollectionChangedEventArgs args)
        {
            CrossCurrentActivity.Current.Activity.RunOnUiThread(() => {action.Invoke(args);});
        }

    }
}
