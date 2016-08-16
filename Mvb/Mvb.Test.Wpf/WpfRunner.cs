using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Mvb.Core.Abstract;

namespace Mvb.Test.Wpf
{
    public class WpfRunner : IUiRunner
    {

        public void Run(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }

        public void Run(Action<NotifyCollectionChangedEventArgs> action, NotifyCollectionChangedEventArgs args)
        {
            Application.Current.Dispatcher.Invoke(()=> action(args));
        }
    }
}
