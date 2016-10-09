using Foundation;
using System;
using UIKit;
using Mvb.FakeContacts.Domain;

namespace Mvb.FakeContacts.iOS.App
{
    public partial class ContactRowCell : UITableViewCell
    {
        public ContactRowCell (IntPtr handle) : base (handle)
        {
        }

		internal void UpdateData(Contact contact)
		{
			this.TextLabel.Text = contact.Name;
			this.ImageView.Image = ByteArrayToImage(contact.AvatarBytes);
		}


		private UIImage ByteArrayToImage(byte[] data)
		{
			if (data == null) return UIImage.FromBundle("glass");
			UIImage image = null;
			try
			{
				image = new UIImage(NSData.FromArray(data));
			}
			catch (Exception)
			{
				return null;
			}
			return image;
		}
	}
}