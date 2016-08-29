using System;
using System.Collections.Generic;
using AppKit;
using Foundation;
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
	
}
