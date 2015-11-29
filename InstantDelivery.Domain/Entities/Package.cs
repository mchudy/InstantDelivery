using InstantDelivery.Common.Enums;
using PropertyChanged;
using System.ComponentModel.DataAnnotations;

namespace InstantDelivery.Domain.Entities
{
    /// <summary>
    /// Klasa reprezentująca paczkę
    /// </summary>
    [ImplementPropertyChanged]
    public class Package : ValidationBase
    {
        /// <summary>
        /// Adres dostawy paczki
        /// </summary>
        public Address ShippingAddress { get; set; } = new Address();
        /// <summary>
        /// Waga paczki
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        [Range(1, 150, ErrorMessage = "Paczka posiada niedopuszczalną wagę")]
        public decimal Weight { get; set; }
        /// <summary>
        /// Wysokość paczki
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        [Range(1, 100, ErrorMessage = "Paczka posiada niedopuszczalną wysokość")]
        public double Height { get; set; }
        /// <summary>
        /// Szerokość paczki
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        [Range(1, 100, ErrorMessage = "Paczka posiada niedopuszczalną szerokość")]
        public double Width { get; set; }
        /// <summary>
        /// Długość paczki
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        [Range(1, 250, ErrorMessage = "Paczka posiada niedopuszczalną długość")]
        public double Length { get; set; }
        /// <summary>
        /// Koszt paczki
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        public decimal Cost { get; set; }
        /// <summary>
        /// Status paczki
        /// </summary>
        public PackageStatus Status { get; set; }
    }
}