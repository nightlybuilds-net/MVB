using System;
using Mvb.Core.Base;
using Mvb.Core.Components.BinderActions;

namespace Mvb.FakeContacts.ModelBinders
{
    public class FakeContactsBinderbase : MvbBase
    {
        public BinderActions<Exception> OnError;

        public FakeContactsBinderbase()
        {
            this.OnError = new BinderActions<Exception>();
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return this._isBusy; }
            set { this.SetProperty(ref this._isBusy, value); }
        }
    }
}
