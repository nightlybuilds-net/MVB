using System;
using System.Collections.Generic;
using Foundation;
using Mvb.FakeContacts.Domain;
using UIKit;

namespace Mvb.FakeContacts.iOS.App
{
	public class ContactsTableDataSource : UITableViewSource
	{
		IList<Contact> _contacts;
		readonly UITableView tableview;

		public ContactsTableDataSource(IList<Contact> contacts, UITableView tableview)
		{
			this.tableview = tableview;
			this._contacts = contacts;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = (ContactRowCell)this.tableview.DequeueReusableCell("contactRow",indexPath);

			if (this._contacts.Count - 1 >= indexPath.Row)
			{
				var contact = this._contacts[indexPath.Row];
				cell.UpdateData(contact);	
			}

			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return this._contacts.Count;
		}
	}


}

