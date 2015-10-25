using System.ComponentModel;

namespace InstantDelivery.Core.Entities
{
    public class Package
    {
        public int PackageId { get; set; }
        public Address ShippingAddress { get; set; }
        public decimal Weight { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        [Description("This constructor is required to create database model of address.")]
        public Package()
        {
            ShippingAddress=new Address();
        }
    }
}