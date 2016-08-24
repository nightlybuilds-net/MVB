using System;
using System.Linq;
using System.Threading.Tasks;
using Mvb.Core.Base;
using Mvb.Core.Components;
using Mvb.Core.Components.BinderActions;
using Mvb.FakeContacts.Abstract;
using Mvb.FakeContacts.Domain;
using Mvb.FakeContacts.ModelBinders.Errors;

namespace Mvb.FakeContacts.ModelBinders
{
	public class ContactsModelBinders : FakeContactsBinderbase
	{
	    public BinderActions<int> OnContactReceived;

		private readonly IContactServices _contactServices;
		private readonly IAvatarServices _avatarServices;

		public ContactsModelBinders(IContactServices contactServices, IAvatarServices avatarServices)
		{
			this._contactServices = contactServices;
			this._avatarServices = avatarServices;
			this.Contacts = new MvbCollection<Contact>();
            this.OnContactReceived = new BinderActions<int>();
			base.InitBinder();
		}

		/// <summary>
		/// Load Contacts
		/// </summary>
		/// <returns></returns>
		public async void LoadContacts(bool loadAvatarAsync = true)
		{
			this.IsBusy = true;
			await Task.Delay(1000);
			await this._contactServices.GetContacts().ContinueWith(contacts =>
			{
				if (contacts.IsFaulted)
                    base.OnError.Invoke(new Exception("error during recovery of the contacts"));
				else
				{
					this.Contacts.Reset(contacts.Result);

					//Notify recovery of contacts to others binders
					MvbMessenger.Send(this, BindersMessages.ContactsLoaded.ToString(), contacts.Result.Count());

                    //Run OnContactReceived UIActions
                    this.OnContactReceived.Invoke(contacts.Result.Count());

                    //now we can load the avatars.. in async mode
                    if (loadAvatarAsync)
					    this.LoadAvatars().ConfigureAwait(false);
				}
				this.IsBusy = false;
			}).ConfigureAwait(false);

		}

		/// <summary>
		/// Shakes the names.
		/// </summary>
		public async void ShakeNames()
		{
			if (!this.Contacts.Any()) return; // must have at least one contact

			await Task.Run(() =>
			{
				foreach (var item in this.Contacts)
					item.ShakeName();
			}).ConfigureAwait(false);

		}


		private async Task LoadAvatars()
		{
			foreach (var contact in this.Contacts)
				await this._avatarServices.LoadAvatarUrl(contact).ConfigureAwait(false);
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
