using System;
using System.Collections.Specialized;

namespace Mvb.Cross.Abstract
{
    class NullUiRunner : IUiRunner
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
