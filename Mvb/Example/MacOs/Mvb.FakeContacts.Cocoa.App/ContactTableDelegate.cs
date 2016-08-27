using System;
using AppKit;
using Foundation;

namespace Mvb.FakeContacts.Cocoa.App
{

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
			NSTableCellView view = (NSTableCellView)tableView.MakeView(CellIdentifier, this);
			if (view == null)
			{
				view = new NSTableCellView();

				view.Identifier = CellIdentifier;
				view.TextField = new NSTextField();
				view.TextField.BackgroundColor = NSColor.Clear;
				view.TextField.Bordered = false;
				view.TextField.Selectable = false;
				view.TextField.Editable = false;

				view.ImageView = new NSImageView();
			}

			var contact = DataSource.ContactsSource[(int)row];

			view.TextField.StringValue = contact.Name;

			if(contact.AvatarBytes != null)
				view.ImageView.Image = new NSImage(NSData.FromArray(contact.AvatarBytes));


			return view;
		}
	}


}

