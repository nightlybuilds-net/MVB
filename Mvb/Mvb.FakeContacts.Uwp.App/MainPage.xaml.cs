using System;
using System.Collections.Specialized;
using System.IO;
using Windows.ApplicationModel.Contacts;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Mvb.FakeContacts.ModelBinders;
using DryIoc;
using Mvb.Core.Args;
using Contact = Mvb.FakeContacts.Domain.Contact;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Mvb.FakeContacts.Uwp.App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ContactsModelBinders _contactsMb;
        private ContactsSummaryModelBinders _contactSummaryMb;

        public MainPage()
        {
            this.InitializeComponent();

            this._contactsMb = App.Ioc.Resolve<ContactsModelBinders>();
            this._contactSummaryMb = App.Ioc.Resolve<ContactsSummaryModelBinders>();

            this.InitModelBinders();
        }
        

        private void LoadBtn_OnClick(object sender, RoutedEventArgs e)
        {
            this._contactsMb.LoadContacts();

        }

        private void ShakeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            this._contactsMb.ShakeNames();

        }

        private void InitModelBinders()
        {
            //Actions for 'IsBusy'
            this._contactsMb.Binder.AddAction<ContactsModelBinders>(b => b.IsBusy, () =>
            {
                this.SummaryBorder.Background = this._contactsMb.IsBusy
                    ? new SolidColorBrush(Colors.Red)
                    : new SolidColorBrush(Colors.Transparent);
                this.LoadBtn.IsEnabled = !this._contactsMb.IsBusy;
            });

            //Actions for 'Summary'
            this._contactSummaryMb.Binder.AddAction<ContactsSummaryModelBinders>(b => b.Summary, () =>
            {
                this.SummaryLbl.Text = this._contactSummaryMb.Summary;
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
                            foreach (var newItem in args.NotifyCollectionChangedEventArgs.NewItems)
                                this.ContactsListView.Items.Add(new ContactRow((( Contact)newItem).Name));
                            this.LoadBtn.Visibility = Visibility.Collapsed;
                            this.ShakeBtn.Visibility = Visibility.Visible;
                            break;
                        case NotifyCollectionChangedAction.Remove:
                        case NotifyCollectionChangedAction.Replace:
                        case NotifyCollectionChangedAction.Move:
                            break;
                        case NotifyCollectionChangedAction.Reset:
                            this.ContactsListView.Items.Clear();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else if (args.MvbUpdateAction == MvbUpdateAction.ItemChanged)
                {
                    var contact = (ContactRow)this.ContactsListView.Items[args.MvbCollectionItemChanged.Index];

                    if (args.MvbCollectionItemChanged.PropertyName == "Name")
                        contact.SetName(args.MvbCollectionItemChanged.NewValue.ToString());
                    else
                    {
                        contact.SetImage((byte[])args.MvbCollectionItemChanged.NewValue);
                    }

                }
            });
        }
    }
}
