using Mvb.Core.Components;

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

            this.Summary = "Hi there.. i'm summary from 'ContactsSummaryModelBinders'";

            this.InitBinder();
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
