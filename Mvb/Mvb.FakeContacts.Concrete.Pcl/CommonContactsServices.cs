using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Mvb.FakeContacts.Abstract;
using Mvb.FakeContacts.Concrete.Pcl.Response;
using Mvb.FakeContacts.Domain;
using Newtonsoft.Json;

namespace Mvb.FakeContacts.Concrete.Pcl
{
    public class CommonContactsServices : IContactService
    {

        public async Task LoadAvatarUrl(Contact contact)
        {
            try
            {
                using (var client = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(15)
                })
                {
                    using (
                        var response = await client.GetAsync("http://uifaces.com/api/v1/random"))
                    {
                        var readStream = new StreamReader(await response.Content.ReadAsStreamAsync(), Encoding.UTF8);
                        var stringContent = readStream.ReadToEnd();

                        var uiResponse = JsonConvert.DeserializeObject<UiFaceResponse>(stringContent);

                        contact.AvatarUrl = uiResponse.image_urls.normal;
                    }
                }
            }
            catch (Exception ex)
            {
                //todo log/manage exception
                throw;
            }
        }
    }
}
