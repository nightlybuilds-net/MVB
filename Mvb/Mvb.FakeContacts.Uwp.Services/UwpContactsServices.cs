using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Mvb.FakeContacts.Abstract;
using Contact = Mvb.FakeContacts.Domain.Contact;

namespace Mvb.FakeContacts.Uwp.Services
{
    public class UwpContactsServices : IContactServices
    {
        public async Task<IEnumerable<Contact>> GetContacts()
        {
            
            var contactStore = await ContactManager.RequestStoreAsync();
            var contacts = await contactStore.FindContactsAsync();

            return contacts.Select(contact => new Contact
            {
                Name = contact.Name
            }).ToList();

            //return new List<Contact>
            //{
            //    new Contact
            //    {
            //        Name = "Pio"
            //    }
            //};
        }
    }
}
