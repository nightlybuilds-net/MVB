using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DryIoc;
using Mvb.Core.Args;
using Mvb.FakeContacts.Domain;
using Mvb.FakeContacts.ModelBinders;
using ToastNotifications;

namespace Mvb.FakeContacts.Wpf.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ContactsModelBinders _contactsMb;
        private readonly ContactsSummaryModelBinders _contactSummaryMb;

        public MainWindow()
        {
            this.InitializeComponent();

            this._contactsMb = App.Ioc.Resolve<ContactsModelBinders>();
            this._contactSummaryMb = App.Ioc.Resolve<ContactsSummaryModelBinders>();

            //INit Notification
            this.NotificationTray.NotificationsSource = new NotificationsSource();

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


        private Action _onIsBusy;
        private Action _onSummary;
        private Action<int> _onContactReceived;
        private Action<MvbCollectionUpdateArgs> _onContacts;
       

        /// <summary>
		/// Inits the model binders.
		/// </summary>
		private void InitModelBinders()
        {
            //Actions for 'IsBusy'
            this._onIsBusy = () =>
            {
                this.SummaryLbl.Background = this._contactsMb.IsBusy ? Brushes.Red : Brushes.Transparent;
                this.LoadBtn.IsEnabled = !this._contactsMb.IsBusy;

                if (this._contactsMb.IsBusy)
                    this.NotificationTray.NotificationsSource.Show("I'm loading your contacts..", NotificationType.Information);
            };
            this._contactsMb.Binder.AddAction<ContactsModelBinders>(this,b => b.IsBusy, this._onIsBusy);

            //Actions for 'Summary'
            this._onSummary = () =>
            {
                this.SummaryLbl.Content = this._contactSummaryMb.Summary;
            };
            this._contactSummaryMb.Binder.AddAction<ContactsSummaryModelBinders>(this,b => b.Summary, this._onSummary);

            //MANUAL RUN FOR FIRST ASSIGNMENT
            this._contactSummaryMb.Binder.Run<ContactsSummaryModelBinders>(b => b.Summary);

            //Actions for 'Contacts' collections
            this._onContacts = args =>
            {
                if (args.MvbUpdateAction == MvbUpdateAction.CollectionChanged)
                {
                    switch (args.NotifyCollectionChangedEventArgs.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                            foreach (var newItem in args.NotifyCollectionChangedEventArgs.NewItems)
                                this.ContactsListView.Items.Add(new ContactRow(((Contact) newItem).Name));
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
                    var contact = (ContactRow) this.ContactsListView.Items[args.MvbCollectionItemChanged.Index];

                    if (args.MvbCollectionItemChanged.PropertyName == "Name")
                        contact.ContactName.Content = args.MvbCollectionItemChanged.NewValue;
                    else
                    {
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.StreamSource = new MemoryStream((byte[]) args.MvbCollectionItemChanged.NewValue);
                        image.EndInit();
                        contact.ContactImage.Source = image;
                    }

                }
            };
            this._contactsMb.Binder.AddActionForCollection<ContactsModelBinders>(this,b => b.Contacts, this._onContacts);

            //MvbAction
            this._onContactReceived = i =>
            {
                this.NotificationTray.NotificationsSource.Show($"MvbActions are Awesome! There are {i} contacts!",NotificationType.Success);
            };
            this._contactsMb.OnContactReceived.AddAction(this, this._onContactReceived);
        }


        //SOMETHING COULD BE TOTALLY NOT ON MVB
        private void LinkLabel_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start("http://www.markjackmilian.net");
        }

        private void LinkLabel_OnMouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void LinkLabel_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }
        //SOMETHING COULD BE TOTALLY NOT ON MVB
    }
}
