using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PropertyChanged;

namespace InstantDelivery.Core.Entities
{
    [ImplementPropertyChanged]
    public class Package : ValidationBase
    {
        public Package()
        {
            ShippingAddress = new Address();
        }

        public Address ShippingAddress { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Range(1, 150, ErrorMessage = "Paczka posiada niedopuszczalną wagę")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Range(1, 100, ErrorMessage = "Paczka posiada niedopuszczalną wysokość")]
        public double Height { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Range(1, 100, ErrorMessage = "Paczka posiada niedopuszczalną szerokość")]
        public double Width { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Range(1, 250, ErrorMessage = "Paczka posiada niedopuszczalną długość")]
        public double Length { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
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