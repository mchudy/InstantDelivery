using InstantDelivery.Common.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;

namespace InstantDelivery.Model.Customers
{
    public class CustomerRegisterDto
    {
        [Required]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        [Compare(nameof(ConfirmPassword))]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        public AddressDto Address { get; set; } = new AddressDto();
    }
}