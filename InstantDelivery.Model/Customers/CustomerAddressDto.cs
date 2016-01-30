namespace InstantDelivery.Model.Customers
{
    public class CustomerAddressDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressDto Address { get; set; }
    }
}
