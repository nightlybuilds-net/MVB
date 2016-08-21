using System;
using Mvb.Core.Abstract;
using Mvb.Core.Args;
using Plugin.CurrentActivity;

namespace Mvb.Platform.Droid
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
