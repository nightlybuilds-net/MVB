using System;

using AppKit;
using Foundation;
using DryIoc;
using Mvb.FakeContacts.ModelBinders;
using Mvb.Core;
using Mvb.FakeContacts.Abstract;
using Mvb.FakeContacts.Concrete;
using Mvb.FakeContacts.Cocoa.Services;
using Mvb.Core.Args;
using System.Collections.Specialized;

namespace Mvb.FakeContacts.Cocoa.App
{
	public partial class ViewController : NSViewController
	{
		ContactsSummaryModelBinders _contactSummaryMb;
		ContactsModelBinders _contactsMb;


		public ViewController(IntPtr handle) : base(handle)
		{
			//Mvb Init
			UiRunnerDispenser.RegisterRunner(() => new MacOsUiRunner());

			//Container
			AppDelegate.Ioc = new Container();

			//Services
			AppDelegate.Ioc.Register<IContactServices, CocoaContactServices>();
			AppDelegate.Ioc.Register<IAvatarServices, CommonAvatarServices>();

			//ModelBinders
			AppDelegate.Ioc.Register<ContactsSummaryModelBinders>(Reuse.Singleton);
			AppDelegate.Ioc.Register<ContactsModelBinders>(Reuse.Singleton);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Do any additional setup after loading the view.

			//Get used binders for this view
			this._contactSummaryMb = AppDelegate.Ioc.Resolve<ContactsSummaryModelBinders>();
			this._contactsMb = AppDelegate.Ioc.Resolve<ContactsModelBinders>();
			//------------------------------

			this.InitModelBinders();

			var dataSource = new ContactTableDataSource(this._contactsMb.Contacts);
			this.ContactsList.DataSource = dataSource;
			this.ContactsList.Delegate = new ContactTableDelegate(dataSource);

		}

		public override NSObject RepresentedObject
		{
			get
			{
				return base.RepresentedObject;
			}
			set
			{
				base.RepresentedObject = value;
				// Update the view, if already loaded.
			}
		}

		partial void LoadContactsClicked(NSObject sender)
		{
			this._contactsMb.LoadContacts();
		}



		void InitModelBinders()
		{
			//Actions for 'IsBusy'
			this._contactsMb.Binder.AddAction<ContactsModelBinders>(b => b.IsBusy, () =>
				{
					this.SummaryLbl.BackgroundColor = this._contactsMb.IsBusy ? NSColor.Red : NSColor.Control;
					this.LoadContactsBtn.Enabled = !this._contactsMb.IsBusy;
				});

			//Actions for 'Summary'
			this._contactSummaryMb.Binder.AddAction<ContactsSummaryModelBinders>(b => b.Summary, () =>
			  {
					this.SummaryLbl.StringValue = this._contactSummaryMb.Summary;
			  });
			//MANUAL RUN FOR FIRST ASSIGNMENT
			this._contactSummaryMb.Binder.Run<ContactsSummaryModelBinders>(b => b.Summary);

			//Actions for 'Contacts' collections
			this._contactsMb.Binder.AddActionForCollection<ContactsModelBinders>(b => b.Contacts, args =>
			  {
				  if (args.MvbUpdateAction == MvbUpdateAction.CollectionChanged)
				  {
					  switch (args.NotifyCollectionChangedEventArgs.Action)
					  {
						  case NotifyCollectionChangedAction.Add:
								this.ContactsList.ReloadData();

							  //this.LoadContactsBtn.Hidden = true;
							  //this.ShakeBtn.Hidden = false;
							  break;
						  case NotifyCollectionChangedAction.Remove:
							  break;
						  case NotifyCollectionChangedAction.Replace:
							  break;
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
					  this.ContactsList.ReloadData();
				  }
			  });


		}



	}
}
