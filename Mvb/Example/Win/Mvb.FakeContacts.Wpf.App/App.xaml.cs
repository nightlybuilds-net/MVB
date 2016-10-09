using System.Windows;
using DryIoc;
using Mvb.FakeContacts.Abstract;
using Mvb.FakeContacts.Concrete;
using Mvb.FakeContacts.ModelBinders;
using Mvb.FakeContacts.Win.Services;
using Mvb.Platform.Win.Wpf;

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

            MvbPlatform.Init();

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
