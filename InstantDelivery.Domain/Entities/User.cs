using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InstantDelivery.Domain.Entities
{
    /// <summary>
    /// Klasa reprezentująca użytkownika.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// Generuje tożsamość użytkownika
        /// </summary>
        /// <param name="manager">Obiekt menadżera użytkowników</param>
        /// <param name="authenticationType">Rodzaj autentykacji</param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
    }
}
