using System;
using System.Collections.Specialized;
using Mvb.Core.Abstract;

namespace Mvb.Test.WinForm
{
    public class NullRunner : IUiRunner
    {
     
        public void Run(Action action)
        {
            action.Invoke();
        }

        public void Run(Action<NotifyCollectionChangedEventArgs> action, NotifyCollectionChangedEventArgs args)
        {
            action.Invoke(args);
        }
    }
}