using System;
using System.Windows;
using Mvb.Core.Abstract;
using Mvb.Core.Args;

namespace Mvb.Platform.Win.Wpf
{
    public class WpfUiRunner : IUiRunner
    {
        public void Run(Action action)
        {
            Application.Current.Dispatcher.Invoke(action.Invoke);
        }

        public void Run(Action<MvbCollectionUpdateArgs> action, MvbCollectionUpdateArgs args)
        {
            Application.Current.Dispatcher.Invoke(action,args);
        }

    }
}
