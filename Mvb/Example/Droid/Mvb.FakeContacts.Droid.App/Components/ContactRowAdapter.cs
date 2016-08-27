using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using FFImageLoading;
using FFImageLoading.Views;
using FFImageLoading.Work;
using Mvb.FakeContacts.Domain;

namespace Mvb.FakeContacts.Droid.App.Components
{
	public class ContactRowAdapter : ArrayAdapter<Contact>
	{
		public ContactRowAdapter(Context context, int textViewResourceId, IList<Contact> objects) : base(context, textViewResourceId, objects)
		{
		}

		public override long GetItemId(int position)
		{
			return position;
		}


		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			ContactViewHolder holder;
			if (convertView == null)
			{
				convertView = LayoutInflater.From(parent.Context).Inflate(
					Resource.Layout.ListRow, parent, false);
				holder = new ContactViewHolder
				{
					Name = convertView.FindViewById<TextView>(Resource.Id.ContactName),
					Avatar = convertView.FindViewById<ImageViewAsync>(Resource.Id.ContactImage)
				};

				convertView.Tag = holder;
			}


			holder = (ContactViewHolder)convertView.Tag;
			var item = this.GetItem(position);

			holder.Name.Text = item.Name;

			if (item.AvatarBytes == null)
				ImageService
				   .Instance
				   .LoadCompiledResource("spy")
				   .IntoAsync(holder.Avatar);
			else
				ImageService
				   .Instance
                   .LoadStream(ct => Task.FromResult<Stream>(new MemoryStream(item.AvatarBytes)))
				   .ErrorPlaceholder("error", ImageSource.CompiledResource)
                   .Error(exception =>
                   {
                       item.AvatarBytes = null;
                   })
                   .Into(holder.Avatar);

			return convertView;
		}





	}
}