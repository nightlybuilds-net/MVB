using System.Linq;
using System.Threading.Tasks;
using Mvb.Core.Base;
using Mvb.Core.Components;
using Mvb.FakeContacts.Abstract;
using Mvb.FakeContacts.Domain;
using Mvb.FakeContacts.ModelBinders.Errors;

namespace Mvb.FakeContacts.ModelBinders
{
    public class ContactsModelBinders : FakeContactsBinderbase
    {
        private readonly IContactServices _contactServices;
        private readonly IAvatarServices _avatarServices;

        public ContactsModelBinders(IContactServices contactServices, IAvatarServices avatarServices)
        {
            this._contactServices = contactServices;
            this._avatarServices = avatarServices;
            this.Contacts = new MvbCollection<Contact>();
            base.InitBinder();
        }

        /// <summary>
        /// Load Contacts
        /// </summary>
        /// <returns></returns>
        public async Task LoadContacts()
        {
            await this._contactServices.GetContacts().ContinueWith(contacts =>
            {
                if(contacts.IsFaulted)
                    base.OnErroraised(new ModelBindersErrorArgs("error during recovery of the contacts"));
                else
                {
                    this.Contacts.AddRange(contacts.Result);
                    //Notify recovery of contacts to others binders
                    MvbMessenger.Send(this, "loadedContacts", contacts.Result.Count());
                }
            });
        }

        #region PROPERTIES
        private MvbCollection<Contact> _contacts;

        public MvbCollection<Contact> Contacts
        {
            get { return this._contacts; }
            set { this.SetProperty(ref this._contacts, value); }
        } 
        #endregion
    }
}
