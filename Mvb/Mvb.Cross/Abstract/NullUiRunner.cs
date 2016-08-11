using System;
using System.Collections.Specialized;
using Mvb.Cross.Args;

namespace Mvb.Cross.Abstract
{
    class NullUiRunner : IUiRunner
    {
        public void Run(Action action)
        {
            action.Invoke();
        }

        public void Run(Action<MvbCollectionUpdateArgs> action, MvbCollectionUpdateArgs args)
        {
            action.Invoke(args);
        }
    }
}
