using InstantDelivery.Common.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace InstantDelivery.Model
{
    /// <summary>
    /// Obiekt DTO reprezentujący użytkownika.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Nazwa użytkownika
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Rola
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Role Role { get; set; }

        /// <summary>
        /// Imię
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Nazwisko
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Adres email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// ID użytkownika
        /// </summary>
        public string Id { get; set; }
    }
}
