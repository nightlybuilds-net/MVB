﻿using System;
using Mvb.Cross.Abstract;
using Mvb.Cross.Args;
using UIKit;

namespace Mvb.Ios
{
    public class IosUiRunner : IUiRunner
    {
        public void Run(Action action)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(action);
        }

        public void Run(Action<MvbCollectionUpdateArgs> action, MvbCollectionUpdateArgs args)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(()=> {action.Invoke(args);});
        }

    }
}
