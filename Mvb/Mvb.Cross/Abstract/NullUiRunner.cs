using System;
using Mvb.Core.Args;

namespace Mvb.Core.Abstract
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
