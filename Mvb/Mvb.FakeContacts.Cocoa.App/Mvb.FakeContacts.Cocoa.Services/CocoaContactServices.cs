using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mvb.FakeContacts.Abstract;
using Mvb.FakeContacts.Domain;

namespace Mvb.FakeContacts.Cocoa.Services
{
	public class CocoaContactServices : IContactServices
	{

		public async Task<IEnumerable<Contact>> GetContacts()
		{
			return await Task.Run(() =>
			{
				return new List<Contact>
				{
					new Contact
					{
						Name = "Pippo"	
					}
				};

			});
		}
	}
}

