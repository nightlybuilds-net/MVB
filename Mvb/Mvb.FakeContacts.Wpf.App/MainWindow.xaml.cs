using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DryIoc;
using Mvb.Core.Args;
using Mvb.FakeContacts.Domain;
using Mvb.FakeContacts.ModelBinders;

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


        /// <summary>
		/// Inits the model binders.
		/// </summary>
		private void InitModelBinders()
        {
            //Actions for 'IsBusy'
            this._contactsMb.Binder.AddAction<ContactsModelBinders>(b => b.IsBusy, () =>
            {
                this.SummaryLbl.Background = this._contactsMb.IsBusy ? Brushes.Red : Brushes.Transparent;
                this.LoadBtn.IsEnabled = !this._contactsMb.IsBusy;
            });

            //Actions for 'Summary'
            this._contactSummaryMb.Binder.AddAction<ContactsSummaryModelBinders>(b => b.Summary, () =>
            {
                this.SummaryLbl.Content = this._contactSummaryMb.Summary;
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
                                this.ContactsListView.Items.Add(new ContactRow(((Contact)newItem).Name));
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
                        contact.ContactName.Content = args.MvbCollectionItemChanged.NewValue;
                    else
                    {
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.StreamSource = new MemoryStream((byte[])args.MvbCollectionItemChanged.NewValue);
                        image.EndInit();
                        contact.ContactImage.Source = image;
                    }
                    
                }
            });

        }
    }
}
