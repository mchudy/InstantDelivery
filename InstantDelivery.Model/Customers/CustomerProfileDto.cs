using InstantDelivery.Common.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace InstantDelivery.Model.Customers
{
    public class CustomerProfileDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RankDescription { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public AddressDto Address { get; set; }
    }
}
