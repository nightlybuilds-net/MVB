using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AddressBook;
using Mvb.FakeContacts.Abstract;
using Mvb.FakeContacts.Domain;

namespace Mvb.FakeContacts.iOS.Services
{
	public class IosContactsServices : IContactServices
	{
		public async Task<IEnumerable<Contact>> GetContacts()
		{
			return await Task.Run(() =>
			{
				var res = new List<Contact>();


				var status = ABAddressBook.GetAuthorizationStatus();
				if (status == ABAuthorizationStatus.Denied || status == ABAuthorizationStatus.Restricted)
					return res;
				else
				{

					var addressBook = new ABAddressBook();


					var valorizeRes = new Action(() => 
					{
						var peoples = addressBook.GetPeople();

						foreach (var item in peoples)
						{
							res.Add(new Contact
							{
								Name = $"{item.FirstName} {item.LastName}"
							});
						}
					});


					if (status == ABAuthorizationStatus.NotDetermined)
					{
						addressBook.RequestAccess((granted, e) =>
					   {
						   if (granted)
								valorizeRes.Invoke();
						   
					   });
					}
					else
					{
						valorizeRes.Invoke();
					}

					return res;
				}
			});

		}
	}
}

