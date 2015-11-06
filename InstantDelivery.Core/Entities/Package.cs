using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InstantDelivery.Core.Entities
{
    public class Package : Entity
    {
        public Package()
        {
            ShippingAddress = new Address();
        }

        public Address ShippingAddress { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Range(0, 150, ErrorMessage = "Paczka przekracza maksymalną dopuszczalną wagę")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Range(0, 100, ErrorMessage = "Paczka przekracza maksymalną dopuszczalną wysokość")]
        public double Height { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Range(0, 100, ErrorMessage = "Paczka przekracza maksymalną dopuszczalną szerokość")]
        public double Width { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Range(0, 250, ErrorMessage = "Paczka przekracza maksymalną dopuszczalną długość")]
        public double Length { get; set; }

        public decimal Cost { get; set; }

        public PackageStatus Status { get; set; }
    }

    public enum PackageStatus
    {
        [Description("Nowa")]
        New,

        [Description("W dostawie")]
        InDelivery,

        [Description("Dostarczona")]
        Delivered
    }
}