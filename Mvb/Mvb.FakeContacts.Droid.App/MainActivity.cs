using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Mvb.FakeContacts.ModelBinders;
using DryIoc;

namespace Mvb.FakeContacts.Droid.App
{
    [Activity(Label = "Mvb.FakeContacts.Droid.App", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private ContactsSummaryModelBinders _contactSummaryMb;
        private ContactsModelBinders _contactsMb;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Main);

            this._contactSummaryMb = MainApplication.Ioc.Resolve<ContactsSummaryModelBinders>();
            this._contactsMb = MainApplication.Ioc.Resolve<ContactsModelBinders>();

            var summaryText = this.FindViewById<TextView>(Resource.Id.SummaryTw);
            var reloadButton = this.FindViewById<Button>(Resource.Id.ReloadBtn);

            reloadButton.Click += (sender, args) =>
            {
                this._contactsMb.LoadContacts();
            };

            this._contactsMb.Binder.AddAction<ContactsModelBinders>(b => b.IsBusy, () =>
            {
                summaryText.SetBackgroundColor(this._contactsMb.IsBusy ? Color.Red : Color.Transparent);
            });

            this._contactSummaryMb.Binder.AddAction<ContactsSummaryModelBinders>(b=>b.Summary, () =>
            {
                summaryText.Text = this._contactSummaryMb.Summary;
            });
            this._contactSummaryMb.Binder.Run<ContactsSummaryModelBinders>(b => b.Summary);

        }
    }
}

