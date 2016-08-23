using System;
using System.Collections.Generic;
using AppKit;
using Mvb.FakeContacts.Domain;

namespace Mvb.FakeContacts.Cocoa.App
{
	public class ContactTableDataSource : NSTableViewDataSource
	{
		public  IList<Contact> ContactsSource;

		public ContactTableDataSource(IList<Contact> contactsSource)
		{
			this.ContactsSource = contactsSource;
		}

		public override nint GetRowCount(NSTableView tableView)
		{
			return ContactsSource.Count;
		}
	}


	public class ContactTableDelegate : NSTableViewDelegate
	{
		const string CellIdentifier = "contactCell";
		ContactTableDataSource DataSource;

		public ContactTableDelegate(ContactTableDataSource datasource)
		{
			this.DataSource = datasource;
		}

		public override NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, nint row)
		{
			//ContactRow view = (ContactRow)tableView.MakeView(CellIdentifier, this);
			//if (view == null)
			//{
			//	view = new ContactRow();
			//}

			NSTextField view = (NSTextField)tableView.MakeView(CellIdentifier, this);
			if (view == null)
			{
				view = new NSTextField();
				view.Identifier = CellIdentifier;
				view.BackgroundColor = NSColor.Clear;
				view.Bordered = false;
				view.Selectable = false;
				view.Editable = false;
			}

			view.StringValue = DataSource.ContactsSource[(int)row].Name;


			// Setup view based on the column selected
			//switch (tableColumn.Title)
			//{
			//	case "Product":
			//		view.StringValue = DataSource.ContactsSource[(int)row].Name;
			//		break;
			//	case "Details":
			//		view.StringValue = DataSource.ContactsSource[(int)row].Name;
			//		break;
			//}

			return view;
		}
	}


	public class ContactRow : NSControl
	{
		public NSTextField Name;
	}
}

