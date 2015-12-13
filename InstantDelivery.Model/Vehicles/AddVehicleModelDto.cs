using System.ComponentModel.DataAnnotations;

namespace InstantDelivery.Model.Vehicles
{
    /// <summary>
    /// Oobiekt DTO modelu pojazdu zawierający dane umożliwiające dodanie go do bazy danych.
    /// </summary>
    public class AddVehicleModelDto : ValidationBase
    {
        /// <summary>
        /// Marka
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Brand { get; set; }

        /// <summary>
        /// Model
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Model { get; set; }

        /// <summary>
        /// Ładowność
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Wartość musi być liczbą całkowitą")]
        public double Payload { get; set; }

        /// <summary>
        /// Dostępna przestrzeń X
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        public double AvailableSpaceX { get; set; }

        /// <summary>
        /// Dostępna przestrzeń Y
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        public double AvailableSpaceY { get; set; }

        /// <summary>
        /// Dostępna przestrzeń Z
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        public double AvailableSpaceZ { get; set; }
    }
}
