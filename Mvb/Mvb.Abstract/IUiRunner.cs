using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mvb.Abstract
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
