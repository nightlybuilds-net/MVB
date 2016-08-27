using System;
using System.Threading.Tasks;
using Mvb.Core.Args;

namespace Mvb.Core.Abstract
{
    public interface IUiRunner
    {
        /// <summary>
        /// Execute action on UiThread
        /// </summary>
        /// <param name="action"></param>
        void Run(Action action);

        /// <summary>
        /// Execute action on UIThread
        /// </summary>
        /// <param name="action"></param>
        /// <param name="args"></param>
        void Run<T>(Action<T> action, T args);
    }
}
