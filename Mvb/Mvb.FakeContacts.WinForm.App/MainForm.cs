using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Mvb.Core.Args;
using Mvb.FakeContacts.Domain;
using Mvb.FakeContacts.ModelBinders;
using DryIoc;
using Mvb.Platform.Win.WinForms;

namespace Mvb.FakeContacts.WinForm.App
{
    public partial class MainForm : Form
    {
        private readonly ContactsModelBinders _contactsMb;
        private readonly ContactsSummaryModelBinders _contactSummaryMb;

        public static readonly SynchronizationContext SyncContext = SynchronizationContext.Current;
        private ImageList _imagesListSmall;

        public MainForm()
        {
            this.InitializeComponent();

            this._contactsMb = Program.Ioc.Resolve<ContactsModelBinders>();
            this._contactSummaryMb = Program.Ioc.Resolve<ContactsSummaryModelBinders>();

            this._imagesListSmall = new ImageList();
            this.ContactsListView.LargeImageList = new ImageList();
            this.ContactsListView.View = View.LargeIcon;

            this.InitModelBinders();
        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            this._contactsMb.LoadContacts();
        }
        private void ShakeBtn_Click(object sender, EventArgs e)
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
                this.SummaryLbl.MvbInvoke(label =>
                {
                    label.BackColor = this._contactsMb.IsBusy ? Color.Red : Color.Transparent;
                });
                this.LoadBtn.MvbInvoke(button =>
                {
                    button.Enabled = !this._contactsMb.IsBusy;
                });

                //NOTE: notify icon not implement ISynchronizeInvoke => no mvbinvoke
                if (this._contactsMb.IsBusy)
                {
                    this.NotifyIcon.Icon = SystemIcons.Information;
                    this.NotifyIcon.BalloonTipTitle = "Mvb Info";
                    this.NotifyIcon.BalloonTipText = "I'm loading your contacts..";
                    this.NotifyIcon.BalloonTipIcon = ToolTipIcon.Info;

                    this.NotifyIcon.Visible = true;
                    this.NotifyIcon.ShowBalloonTip(3000);
                }
                
            });

            //Actions for 'Summary'
            this._contactSummaryMb.Binder.AddAction<ContactsSummaryModelBinders>(b => b.Summary, () =>
            {
                this.SummaryLbl.MvbInvoke(label =>
                {
                    label.Text = this._contactSummaryMb.Summary;
                });
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
                                this.ContactsListView.MvbInvoke(view =>
                                {
                                    view.LargeImageList.Images.Add(Image.FromFile("glass.png"));
                                    view.Items.Add(new ListViewItem
                                    {
                                        Text = ((Contact) newItem).Name,
                                        ImageIndex = view.LargeImageList.Images.Count-1
                                    });
                                    
                                });
                            this.LoadBtn.MvbInvoke(button =>{button.Visible = false;}); 
                            this.ShakeBtn.MvbInvoke(button =>{button.Visible = true; }); 
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
                    this.ContactsListView.MvbInvoke(view =>
                    {
                        if(args.MvbCollectionItemChanged.PropertyName == "Name")
                            view.Items[args.MvbCollectionItemChanged.Index].Text = args.MvbCollectionItemChanged.NewValue.ToString();
                        else
                        {
                            view.LargeImageList.Images[args.MvbCollectionItemChanged.Index] = Image.FromStream(new MemoryStream((byte[]) args.MvbCollectionItemChanged.NewValue));
                            view.Refresh();
                        }
                    });
                }
            });

            //MvbAction
            this._contactsMb.OnContactReceived.Add(i =>
            {
                this.MvbInvoke(form => MessageBox.Show(form, $"MvbActions are Awesome! There are {i} contacts!")) ;
            });
        }

        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.markjackmilian.net");
        }
    }
}
