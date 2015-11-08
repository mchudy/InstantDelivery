using System.ComponentModel.DataAnnotations;

namespace InstantDelivery.Core.Entities
{
    /// <summary>
    /// Klasa reprezentująca pojazd
    /// </summary>
    public class Vehicle : ValidationBase
    {
        /// <summary>
        /// Numer rejestracyjny pojazdu
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string RegistrationNumber { get; set; }
        /// <summary>
        /// Model pojazdu
        /// </summary>
        public virtual VehicleModel VehicleModel { get; set; }
    }
}
