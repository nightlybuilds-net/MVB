using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mvb.Core.Base;

namespace Mvb.FakeContacts.Dto
{
    public class Contact : Bindable
    {
        private string _name;

        public string Name
        {
            get { return this._name; }
            set { this.SetProperty(ref this._name, value); }
        }

        private string _avatarUrl;

        public string AvatarUrl
        {
            get { return this._avatarUrl; }
            set { this.SetProperty(ref this._avatarUrl, value); }
        }
    }
}
