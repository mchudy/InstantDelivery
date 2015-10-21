namespace InstantDelivery.Core.Entities
{
    public class Package
    {
        public int PackageId { get; set; }
        public Address ShippingAddress { get; set; }
        public decimal Weight { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}