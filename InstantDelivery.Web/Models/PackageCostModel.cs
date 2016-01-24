using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InstantDelivery.Web.Models
{
    public class PackageCostModel
    {
        /// <summary>
        /// Waga paczki
        /// </summary>
        [RegularExpression("\\d+((\\.)\\d+)?", ErrorMessage = "Wprowadź poprawną liczbę")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        [Range(1, 150, ErrorMessage = "Paczka posiada niedopuszczalną wagę")]
        [DisplayName("Waga [kg]")]
        public decimal Weight { get; set; }

        /// <summary>
        /// Wysokość paczki
        /// </summary>
        [RegularExpression("\\d+((\\.)\\d+)?", ErrorMessage = "Wprowadź poprawną liczbę")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        [Range(1, 100, ErrorMessage = "Paczka posiada niedopuszczalną wysokość")]
        [DisplayName("Wysokość [cm]")]
        public double Height { get; set; }

        /// <summary>
        /// Szerokość paczki
        /// </summary>
        [RegularExpression("\\d+((\\.)\\d+)?", ErrorMessage = "Wprowadź poprawną liczbę")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        [Range(1, 100, ErrorMessage = "Paczka posiada niedopuszczalną szerokość")]
        [DisplayName("Szerokość [cm]")]
        public double Width { get; set; }

        /// <summary>
        /// Długość paczki
        /// </summary>
        [RegularExpression("\\d+((\\.)\\d+)?", ErrorMessage = "Wprowadź poprawną liczbę")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        [Range(1, 250, ErrorMessage = "Paczka posiada niedopuszczalną długość")]
        [DisplayName("Długość [cm]")]
        public double Length { get; set; }
    }
}