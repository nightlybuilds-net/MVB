using System.Collections.Generic;
using System.Threading.Tasks;
using Mvb.FakeContacts.Domain;

namespace Mvb.FakeContacts.Abstract
{
    public interface IContactResource
    {
        /// <summary>
        /// Get all contacts
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Contact>> GetContacts();
    }
}
