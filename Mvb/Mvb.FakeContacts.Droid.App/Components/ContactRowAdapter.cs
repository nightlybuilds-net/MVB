using System;
using System.Collections.Generic;
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

			if (string.IsNullOrEmpty(item.AvatarUrl))
				ImageService
				   .Instance
				   .LoadCompiledResource("spy")
				   .IntoAsync(holder.Avatar);
			else
				ImageService
				   .Instance
				   .LoadUrl(item.AvatarUrl, TimeSpan.FromHours(1))
				   .LoadingPlaceholder("glass", ImageSource.CompiledResource)
				   .ErrorPlaceholder("error", ImageSource.CompiledResource)
				   .Error(exception =>
				   {
					   item.AvatarUrl = string.Empty;
				   })
				   .Into(holder.Avatar);

			return convertView;
		}





	}
}