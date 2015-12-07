using System.ComponentModel.DataAnnotations;

namespace InstantDelivery.Domain.Entities
{
    /// <summary>
    /// Klasa reprezentująca pojazd
    /// </summary>
    public class Vehicle : Entity
    {
        /// <summary>
        /// Numer rejestracyjny pojazdu
        /// </summary>
        [Required]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Model pojazdu
        /// </summary>
        public virtual VehicleModel VehicleModel { get; set; }
    }
}
