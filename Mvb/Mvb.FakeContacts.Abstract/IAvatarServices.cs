using System.Threading.Tasks;
using Mvb.FakeContacts.Domain;

namespace Mvb.FakeContacts.Abstract
{
    public interface IAvatarServices
    {
        /// <summary>
        /// Get AvatarBytes for contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>Loade the avatar</returns>
        Task LoadAvatarUrl(Contact contact);
    }
}
