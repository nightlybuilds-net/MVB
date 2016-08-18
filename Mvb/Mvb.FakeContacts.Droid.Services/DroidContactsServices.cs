using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Provider;
using Mvb.FakeContacts.Abstract;
using Mvb.FakeContacts.Domain;
using Plugin.CurrentActivity;

namespace Mvb.FakeContacts.Droid.Services
{
    public class DroidContactsServices : IContactServices
    {
        public async Task<IEnumerable<Contact>> GetContacts()
        {
            return await Task.Run(() =>
            {
                var uri = ContactsContract.Contacts.ContentUri;

                string[] projection = { ContactsContract.Contacts.InterfaceConsts.Id, ContactsContract.Contacts.InterfaceConsts.DisplayName };

                var cursor = CrossCurrentActivity.Current.Activity.ManagedQuery(uri, projection, null, null, null);

                var contactList = new List<Contact>();

                if (cursor.MoveToFirst())
                {
                    do
                    {
                        contactList.Add(new Contact() {Name = cursor.GetString(
                                cursor.GetColumnIndex(projection[1]))});
                    } while (cursor.MoveToNext());
                }

                return contactList;
            });
           
        }
    }
}
