using System;
using System.Threading.Tasks;
using Mvb.Core.Abstract;
using Plugin.CurrentActivity;

namespace Mvb.Platform.Droid
{
    public class DroidUiRunner : IUiRunner
    {
        public void Run(Action action)
        {
            CrossCurrentActivity.Current.Activity.RunOnUiThread(action.Invoke);
        }

        public void Run<T>(Action<T> action, T args)
        {
            CrossCurrentActivity.Current.Activity.RunOnUiThread(() => { action.Invoke(args); });
        }
    }
}
