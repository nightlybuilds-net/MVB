using System.Threading.Tasks;
using Mvb.FakeContacts.Domain;

namespace Mvb.FakeContacts.Abstract
{
    public interface IContactService
    {
        /// <summary>
        /// Get AvatarUrl for contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>Loade the avatar</returns>
        Task LoadAvatarUrl(Contact contact);
    }
}
