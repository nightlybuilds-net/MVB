using System;
using System.Collections.Specialized;
using Mvb.Cross.Abstract;
using Mvb.Cross.Args;
using Plugin.CurrentActivity;

namespace Mvb.Droid
{
    public class DroidUiRunner : IUiRunner
    {
        public void Run(Action action)
        {
            CrossCurrentActivity.Current.Activity.RunOnUiThread(action.Invoke);
        }

        public void Run(Action<MvbCollectionUpdateArgs> action, MvbCollectionUpdateArgs args)
        {
            CrossCurrentActivity.Current.Activity.RunOnUiThread(() => { action.Invoke(args); });
        }

    }
}
