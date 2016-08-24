using System;
using System.Threading.Tasks;

namespace Mvb.Core.Abstract
{
    class NullUiRunner : IUiRunner
    {
        public void Run(Action action)
        {
            action.Invoke();
        }

        public void Run<T>(Action<T> action, T args)
        {
            action.Invoke(args);
        }
    }
}
