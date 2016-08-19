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

namespace Mvb.FakeContacts.Droid.App
{
    [Activity(Label = "Mvb.FakeContacts.Droid.App", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, AbsListView.IOnScrollListener
    {
        private ContactsSummaryModelBinders _contactSummaryMb;
        private ContactsModelBinders _contactsMb;
        private ContactRowAdapter _contactsAdapter;
        private ListView _contactsListView;

        private int _firstVisibleRow;
        private int _lastVisibleRow;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Main);

            this._contactSummaryMb = MainApplication.Ioc.Resolve<ContactsSummaryModelBinders>();
            this._contactsMb = MainApplication.Ioc.Resolve<ContactsModelBinders>();

            var summaryText = this.FindViewById<TextView>(Resource.Id.SummaryTw);
            var reloadButton = this.FindViewById<Button>(Resource.Id.ReloadBtn);

            //init adapter
            this._contactsAdapter = new ContactRowAdapter(this, Android.Resource.Layout.SimpleListItem1,
                this._contactsMb.Contacts);
            this._contactsListView = this.FindViewById<ListView>(Resource.Id.ContactsListView);
            this._contactsListView.SetOnScrollListener(this);
            this._contactsListView.Adapter = this._contactsAdapter;

            reloadButton.Click += (sender, args) =>
            {
                this._contactsMb.LoadContacts();
            };


            #region ModelBinders
            this._contactsMb.Binder.AddAction<ContactsModelBinders>(b => b.IsBusy, () =>
                {
                    summaryText.SetBackgroundColor(this._contactsMb.IsBusy ? Color.Red : Color.Transparent);
                });

            this._contactSummaryMb.Binder.AddAction<ContactsSummaryModelBinders>(b => b.Summary, () =>
              {
                  summaryText.Text = this._contactSummaryMb.Summary;
              });
            //MANUAL RUN FOR FIRST ASSIGNMENT
            this._contactSummaryMb.Binder.Run<ContactsSummaryModelBinders>(b => b.Summary); 

            //BINDER FOR COLLECTION
            this._contactsMb.Binder.AddActionForCollection<ContactsModelBinders>(b=>b.Contacts, args =>
            {
                if (args.MvbUpdateAction == MvbUpdateAction.CollectionChanged)
                {
                    switch (args.NotifyCollectionChangedEventArgs.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                            this._contactsAdapter.AddAll(args.NotifyCollectionChangedEventArgs.NewItems);
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
                    if(args.MvbCollectionItemChanged.Index >= this._firstVisibleRow && args.MvbCollectionItemChanged.Index <= this._lastVisibleRow)
                        this._contactsAdapter.NotifyDataSetChanged();
                }
            });

            #endregion

        }

        public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
        {
            this._firstVisibleRow = this._contactsListView.FirstVisiblePosition;
            this._lastVisibleRow = this._contactsListView.LastVisiblePosition;
        }

        public void OnScrollStateChanged(AbsListView view, ScrollState scrollState)
        {}
    }
}

