using InstantDelivery.Common.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace InstantDelivery.Model
{
    public class UserDto
    {
        public string UserName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Role Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
    }
}
