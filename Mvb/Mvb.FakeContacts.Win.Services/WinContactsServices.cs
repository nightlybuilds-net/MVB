using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mvb.FakeContacts.Abstract;
using Mvb.FakeContacts.Domain;

namespace Mvb.FakeContacts.Win.Services
{
    public class WinContactsServices : IContactServices
    {
        public async Task<IEnumerable<Contact>> GetContacts()
        {
           return await Task.Run(() =>
            {
                if (!File.Exists("contacts.txt")) return new List<Contact>();

                var allLines = File.ReadAllLines("contacts.txt");

                return allLines.Select(line => new Contact
                {
                    Name = line
                }).ToList();
            });
        }
    }
}
