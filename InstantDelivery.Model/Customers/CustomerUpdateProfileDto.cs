using System.ComponentModel.DataAnnotations;

namespace InstantDelivery.Model.Customers
{
    public class CustomerUpdateProfileDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public AddressDto Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}