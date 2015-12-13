using System.ComponentModel.DataAnnotations;

namespace InstantDelivery.Model.Vehicles
{
    /// <summary>
    /// Obiekt DTO zawierający dane pojazdu umożliwiające dodanie w bazie danych.
    /// </summary>
    public class AddVehicleDto : ValidationBase
    {
        /// <summary>
        /// ID pojazdu
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Numer rejestracyjny pojazdu
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// ID modelu pojazdu
        /// </summary>
        public int VehicleModelId { get; set; }
    }
}
