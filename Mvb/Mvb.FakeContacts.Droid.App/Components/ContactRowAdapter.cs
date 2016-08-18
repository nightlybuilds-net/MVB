using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
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
                holder = new ContactViewHolder();
                holder.Name = convertView.FindViewById<TextView>(Resource.Id.ContactName);
                holder.Avatar = convertView.FindViewById<ImageView>(Resource.Id.ContactImage);

                convertView.Tag = holder;
            }


            holder = (ContactViewHolder)convertView.Tag;
            var item = this.GetItem(position);

            holder.Name.Text = item.Name;
            if (!string.IsNullOrEmpty(item.AvatarUrl) && holder.Avatar.Drawable == null)
            {
                var btm = this.GetImageBitmapFromUrl(item.AvatarUrl);
                holder.Avatar.SetImageBitmap(btm); 
            }
            
            return convertView;
        }


        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }

        
    }
}