using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mvb.Cross;
using Mvb.Cross.Abstract;
using RemIoc;

namespace Mvb.Test.WinForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            RIoc.Register<IUiRunner, NullRunner>();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
    }

    public class NullRunner : IUiRunner
    {
     
        public void Run(Action action)
        {
            action.Invoke();
        }
    }
}
