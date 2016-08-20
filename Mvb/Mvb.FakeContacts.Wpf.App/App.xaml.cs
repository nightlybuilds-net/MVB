using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DryIoc;
using Mvb.Core;
using Mvb.FakeContacts.Abstract;
using Mvb.FakeContacts.Concrete;
using Mvb.FakeContacts.ModelBinders;
using Mvb.FakeContacts.Win.Services;
using Mvb = Mvb.Core.Mvb;

namespace Mvb.FakeContacts.Wpf.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IContainer Ioc;
     

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Core.Mvb.NullInit();

            UiRunnerDispenser.RegisterRunner(() => new WpfUiRunner());

            //Init IOC
            Ioc = new Container();

            //Services
            Ioc.Register<IContactServices, WinContactsServices>();
            Ioc.Register<IAvatarServices, CommonAvatarServices>();

            //ModelBinders
            Ioc.Register<ContactsSummaryModelBinders>(Reuse.Singleton);
            Ioc.Register<ContactsModelBinders>(Reuse.Singleton);
        }
    }
}
