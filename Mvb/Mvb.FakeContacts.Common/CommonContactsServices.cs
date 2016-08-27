using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Mvb.FakeContacts.Abstract;
using Mvb.FakeContacts.Domain;

namespace Mvb.FakeContacts.Common
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

                        return JsonConvert.DeserializeObject<IEnumerable<ExtractionDto>>(stringContent);
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
