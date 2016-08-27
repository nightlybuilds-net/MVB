using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Mvb.FakeContacts.Uwp.App
{
    public sealed partial class ContactRow : StackPanel
    {
        public ContactRow(string contactName)
        {
            this.InitializeComponent();
            this.SetName(contactName);
        }

        public void SetName(string contactName)
        {
            this.ContactName.Text = contactName;
        }

        public async void SetImage(byte[] bytes)
        {
            this.ContactImage.Source  = await this.ByteArrayToBitmap(bytes);
        }

        private async Task<BitmapImage> ByteArrayToBitmap(byte[] bytes)
        {
            using (var stream = new InMemoryRandomAccessStream())
            {
                using (var writer = new DataWriter(stream.GetOutputStreamAt(0)))
                {
                    writer.WriteBytes(bytes);
                    await writer.StoreAsync();
                }
                var image = new BitmapImage();
                await image.SetSourceAsync(stream);
                return image;
            }
        }
    }
}
