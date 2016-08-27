using System;
using System.Threading.Tasks;
using System.Windows;
using Mvb.Core.Abstract;

namespace Mvb.Platform.Win.Wpf
{
    public class WpfUiRunner : IUiRunner
    {
        public void Run(Action action)
        {
            Application.Current.Dispatcher.Invoke(action.Invoke);
        }

        public void Run<T>(Action<T> action, T args)
        {
            Application.Current.Dispatcher.Invoke(action,args);
        }
    }
}
