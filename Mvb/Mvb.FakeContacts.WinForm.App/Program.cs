using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DryIoc;
using Mvb.Core;
using Mvb.FakeContacts.Abstract;
using Mvb.FakeContacts.Concrete;
using Mvb.FakeContacts.ModelBinders;
using Mvb.FakeContacts.Win.Services;
using Mvb = Mvb.Core.Mvb;

namespace Mvb.FakeContacts.WinForm.App
{
    static class Program
    {
        public static IContainer Ioc;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //UiRunnerDispenser.RegisterRunner(()=>new WinFormUiRunner());

            Core.Mvb.NullInit();
           
            //Init IOC
            Ioc = new Container();

            //Services
            Ioc.Register<IContactServices, WinContactsServices>();
            Ioc.Register<IAvatarServices, CommonAvatarServices>();

            //ModelBinders
            Ioc.Register<ContactsSummaryModelBinders>(Reuse.Singleton);
            Ioc.Register<ContactsModelBinders>(Reuse.Singleton);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
