using System;

namespace Mvb.Cross
{
    public interface IUiRunner
    {
        /// <summary>
        /// Execute action on UiThread
        /// </summary>
        /// <param name="action"></param>
        void Run(Action action);
    }
}
