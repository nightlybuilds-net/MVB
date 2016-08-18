using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mvb.Core.Components;
using Mvb.FakeContacts.Domain;

namespace Mvb.FakeContacts.ModelBinders
{
    public class ContactsSummaryModelBinders : FakeContactsBinderbase
    {
        public ContactsSummaryModelBinders()
        {
            MvbMessenger.Subscribe<ContactsModelBinders, int>(this, BindersMessages.ContactsLoaded.ToString(), (model, msg) =>
            {
                this.Summary = msg == 0 ? "No contacts found!" : $"There are {msg} contacts!!";
            });
        }


        #region PROPERTIES
        private string _summary;

        public string Summary
        {
            get { return this._summary; }
            set { this.SetProperty(ref this._summary, value); }
        } 
        #endregion
    }
}
