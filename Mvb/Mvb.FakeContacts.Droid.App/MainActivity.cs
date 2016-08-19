using System;
using System.Collections.Specialized;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Mvb.FakeContacts.ModelBinders;
using DryIoc;
using Mvb.Core.Args;
using Mvb.FakeContacts.Domain;
using Mvb.FakeContacts.Droid.App.Components;
using Android.Support.Design.Widget;

namespace Mvb.FakeContacts.Droid.App
{
	[Activity(Label = "Mvb.FakeContacts.Droid.App", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		private ContactsSummaryModelBinders _contactSummaryMb;
		private ContactsModelBinders _contactsMb;
		private ContactRowAdapter _contactsAdapter;
		private ListView _contactsListView;
		private Button _changeBtn;
		private Button _reloadButton;
		private TextView _summaryText;


		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			this.SetContentView(Resource.Layout.Main);

			//Get used binders for this view
			this._contactSummaryMb = MainApplication.Ioc.Resolve<ContactsSummaryModelBinders>();
			this._contactsMb = MainApplication.Ioc.Resolve<ContactsModelBinders>();


			this._summaryText = this.FindViewById<TextView>(Resource.Id.SummaryTw);
			this._reloadButton = this.FindViewById<Button>(Resource.Id.ReloadBtn);
			this._changeBtn = this.FindViewById<Button>(Resource.Id.ChangeNameBtn);


			//init adapter
			this._contactsAdapter = new ContactRowAdapter(this, Android.Resource.Layout.SimpleListItem1,
				this._contactsMb.Contacts);
			this._contactsListView = this.FindViewById<ListView>(Resource.Id.ContactsListView);
			this._contactsListView.Adapter = this._contactsAdapter;

			this._reloadButton.Click += (sender, args) =>
			{
				this._contactsMb.LoadContacts();
			};

			this._changeBtn.Click += (sender, args) =>
			{
				this._contactsMb.ShakeNames();
			};

			InitModelBinders(); //Init the binders
		}

		/// <summary>
		/// Inits the model binders.
		/// </summary>
		private void InitModelBinders()
		{
			#region ModelBinders
			this._contactsMb.Binder.AddAction<ContactsModelBinders>(b => b.IsBusy, () =>
				{
					this._summaryText.SetBackgroundColor(this._contactsMb.IsBusy ? Color.Red : Color.Transparent);
					this._reloadButton.Enabled = !this._contactsMb.IsBusy;

					if (this._contactsMb.IsBusy)
					{
						var snack = Snackbar.Make(this.FindViewById<TextView>(Resource.Id.SignatureTw), "Loading contacts..", Snackbar.LengthShort);
						snack.View.SetBackgroundColor(Color.Red);
						snack.Show();
					}
				});

			this._contactSummaryMb.Binder.AddAction<ContactsSummaryModelBinders>(b => b.Summary, () =>
			  {
				  this._summaryText.Text = this._contactSummaryMb.Summary;
			  });
			//MANUAL RUN FOR FIRST ASSIGNMENT
			this._contactSummaryMb.Binder.Run<ContactsSummaryModelBinders>(b => b.Summary);

			//BINDER FOR COLLECTION
			this._contactsMb.Binder.AddActionForCollection<ContactsModelBinders>(b => b.Contacts, args =>
			  {
				  if (args.MvbUpdateAction == MvbUpdateAction.CollectionChanged)
				  {
					  switch (args.NotifyCollectionChangedEventArgs.Action)
					  {
						  case NotifyCollectionChangedAction.Add:
						  	this._contactsAdapter.AddAll(args.NotifyCollectionChangedEventArgs.NewItems);
						  	this._reloadButton.Visibility = ViewStates.Gone;
							  this._changeBtn.Visibility = ViewStates.Visible;
							  break;
						  case NotifyCollectionChangedAction.Remove:
							  foreach (var item in args.NotifyCollectionChangedEventArgs.OldItems)
								  this._contactsAdapter.Remove((Contact)item);
							  break;
						  case NotifyCollectionChangedAction.Replace:
							  break;
						  case NotifyCollectionChangedAction.Move:
							  break;
						  case NotifyCollectionChangedAction.Reset:
							  this._contactsAdapter.Clear();
							  this._contactsAdapter.NotifyDataSetChanged();
							  break;
						  default:
							  throw new ArgumentOutOfRangeException();
					  }
				  }
				  else if (args.MvbUpdateAction == MvbUpdateAction.ItemChanged)
				  {
					  //here i have index, oldvalue and new value of changed item
					  if (args.MvbCollectionItemChanged.Index >= this._contactsListView.FirstVisiblePosition &&
						  args.MvbCollectionItemChanged.Index <= this._contactsListView.LastVisiblePosition)
						  this._contactsAdapter.NotifyDataSetChanged();
				  }
			  });

			#endregion
		}
	}
}

