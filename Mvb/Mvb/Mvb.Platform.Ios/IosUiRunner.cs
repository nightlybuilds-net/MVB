using System;
using System.Threading.Tasks;
using Mvb.Core.Abstract;
using Mvb.Core.Args;
using UIKit;

namespace Mvb.Platform.Ios
{
    public class IosUiRunner : IUiRunner
    {
        public void Run(Action action)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(action);
        }

        public void Run<T>(Action<T> action, T args)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(()=> {action.Invoke(args);});
        }

    }
}
