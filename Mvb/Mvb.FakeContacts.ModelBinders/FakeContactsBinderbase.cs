using System;
using Mvb.Core.Base;
using Mvb.FakeContacts.ModelBinders.Errors;

namespace Mvb.FakeContacts.ModelBinders
{
    public class FakeContactsBinderbase : MvbBase
    {
        public event EventHandler<ModelBindersErrorArgs> ErrorRaised;

        private bool _isBusy;

        public bool IsBusy
        {
            get { return this._isBusy; }
            set { this.SetProperty(ref this._isBusy, value); }
        }


        protected virtual void OnErroraised(ModelBindersErrorArgs e)
        {
            var handler = this.ErrorRaised;
            handler?.Invoke(this, e);
        }
    }
}
