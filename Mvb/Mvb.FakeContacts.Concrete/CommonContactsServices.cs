using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Mvb.FakeContacts.Abstract;
using Mvb.FakeContacts.Concrete.Pcl.Response;
using Mvb.FakeContacts.Domain;
using Newtonsoft.Json;

namespace Mvb.FakeContacts.Concrete
{
    public class CommonAvatarServices : IAvatarServices
    {

        public async Task LoadAvatarUrl(Contact contact)
        {
            using (var client = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(15)
            })
            {
                using (
                    var response = await client.GetAsync("http://uifaces.com/api/v1/random"))
                {
                    using (var readStream = new StreamReader(await response.Content.ReadAsStreamAsync(), Encoding.UTF8))
                    {
                        var stringContent = readStream.ReadToEnd();
                        var uiResponse = JsonConvert.DeserializeObject<UiFaceResponse>(stringContent);

                        contact.AvatarBytes = await client.GetByteArrayAsync(uiResponse.image_urls.mini);
                    }
                }
            }
        }
    }
}
