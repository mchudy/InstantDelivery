using System.ComponentModel.DataAnnotations;

namespace InstantDelivery.Domain.Entities
{
    /// <summary>
    /// Klasa reprezentująca szczegóły modelu pojazdu
    /// </summary>
    public class VehicleModel : Entity
    {
        /// <summary>
        /// Marka pojazdu
        /// </summary>
        [Required]
        public string Brand { get; set; }

        /// <summary>
        /// Model pojazdu
        /// </summary>
        [Required]
        public string Model { get; set; }

        /// <summary>
        /// Ładowność pojazdu
        /// </summary>
        public double Payload { get; set; }

        /// <summary>
        /// Dostępna przestrzeń-X
        /// </summary>
        public double AvailableSpaceX { get; set; }

        /// <summary>
        /// Dostępna przestrzeńY
        /// </summary>
        public double AvailableSpaceY { get; set; }

        /// <summary>
        /// Dostępna przestrzeń-Z
        /// </summary>
        public double AvailableSpaceZ { get; set; }
    }
}