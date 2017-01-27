using System;
using System.Collections.Specialized;
using System.Diagnostics;
using DryIoc;
using Mvb.Core.Args;
using Mvb.FakeContacts.Abstract;
using Mvb.FakeContacts.Concrete;
using Mvb.FakeContacts.Domain;
using Mvb.FakeContacts.ModelBinders;
using Mvb.FakeContacts.Win.Services;

namespace Mvb.FakeContacts.WinConsole.App
{
    class Program
    {
        private static ContactsSummaryModelBinders _contactSummaryMb;
        private static ContactsModelBinders _contactMb;
        private static object _sub = new object();

        static void Main(string[] args)
        {
            //Mvb Init
            Core.Mvb.NullInit();

            //Init IOC
            var ioc = new Container();

            //Services
            ioc.Register<IContactServices, WinContactsServices>();
            ioc.Register<IAvatarServices, CommonAvatarServices>();

            //ModelBinders
            ioc.Register<ContactsSummaryModelBinders>(Reuse.Singleton);
            ioc.Register<ContactsModelBinders>(Reuse.Singleton);
            //-------------------------------------------

            _contactMb = ioc.Resolve<ContactsModelBinders>();
            _contactSummaryMb = ioc.Resolve<ContactsSummaryModelBinders>();

            Console.WriteLine("Mvb & Ioc Init..");
            InitModelBinders();
            Console.WriteLine("Press a button to load contacts..");
            Console.ReadLine();

            _contactMb.LoadContacts(false);

            Console.WriteLine("Press a button to shake names..");
            Console.ReadLine();

            _contactMb.ShakeNames();

            Console.WriteLine("Press a button to open my awesome website..");
            Console.ReadLine();

            Process.Start("http://www.markjackmilian.net");
        }



        /// <summary>
        /// Inits the model binders.
        /// </summary>
        private static void InitModelBinders()
        {
            //Actions for 'IsBusy'
            _contactMb.Binder.AddAction<ContactsModelBinders>(_sub, b => b.IsBusy, () =>
            {
                if(_contactMb.IsBusy)
                    Console.WriteLine("Loading contacts..");
            });

            //Actions for 'Summary'
            _contactSummaryMb.Binder.AddAction<ContactsSummaryModelBinders>(_sub, b => b.Summary, () =>
            {
                Console.WriteLine(_contactSummaryMb.Summary);
            });
            //MANUAL RUN FOR FIRST ASSIGNMENT
            _contactSummaryMb.Binder.Run<ContactsSummaryModelBinders>(b => b.Summary);

            //Actions for 'Contacts' collections
            _contactMb.Binder.AddActionForCollection<ContactsModelBinders>(_sub, b => b.Contacts, args =>
            {
                if (args.MvbUpdateAction == MvbUpdateAction.CollectionChanged)
                {
                    switch (args.NotifyCollectionChangedEventArgs.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                            foreach (var newItem in args.NotifyCollectionChangedEventArgs.NewItems)
                                Console.WriteLine("New Contact => {0}",((Contact)newItem).Name);
                            break;
                        case NotifyCollectionChangedAction.Remove:
                        case NotifyCollectionChangedAction.Replace:
                        case NotifyCollectionChangedAction.Move:
                            break;
                        case NotifyCollectionChangedAction.Reset:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else if (args.MvbUpdateAction == MvbUpdateAction.ItemChanged)
                {
                    if (args.MvbCollectionItemChanged.PropertyName == "Name")
                        Console.WriteLine("Contact has changed his name from {0} to {1}",args.MvbCollectionItemChanged.OldValue, args.MvbCollectionItemChanged.NewValue);
                   
                }
            });

            _contactMb.OnContactReceived.AddAction(_sub, i =>
            {
                Console.WriteLine($"MvbActions are Awesome! There are {i} contacts!");
            });

        }
    }
}
