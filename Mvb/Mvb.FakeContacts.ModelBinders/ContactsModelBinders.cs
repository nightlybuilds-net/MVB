using Mvb.Core.Base;
using Mvb.Core.Components;
using Mvb.FakeContacts.ModelBinders.Dto;

namespace Mvb.FakeContacts.ModelBinders
{
    public class ContactsModelBinders : MvbBase
    {

        public ContactsModelBinders()
        {
            this.Contacts = new MvbCollection<Contact>();
            base.InitBinder();
        }

        private MvbCollection<Contact> _contacts;

        public MvbCollection<Contact> Contacts
        {
            get { return this._contacts; }
            set { this.SetProperty(ref this._contacts, value); }
        }
    }
}
