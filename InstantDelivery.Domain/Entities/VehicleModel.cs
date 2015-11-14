using System.ComponentModel.DataAnnotations;

namespace InstantDelivery.Domain.Entities
{
    /// <summary>
    /// Klasa reprezentująca szczegóły modelu pojazdu
    /// </summary>
    public class VehicleModel : ValidationBase
    {
        /// <summary>
        /// Marka pojazdu
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Brand { get; set; }
        /// <summary>
        /// Model pojazdu
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Model { get; set; }
        /// <summary>
        /// Ładowność pojazdu
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Wartość musi być liczbą całkowitą")]
        public double Payload { get; set; }
        /// <summary>
        /// Dostępna przestrzeń-X
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        public double AvailableSpaceX { get; set; }
        /// <summary>
        /// Dostępna przestrzeńY
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        public double AvailableSpaceY { get; set; }
        /// <summary>
        /// Dostępna przestrzeń-Z
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        public double AvailableSpaceZ { get; set; }
    }
}