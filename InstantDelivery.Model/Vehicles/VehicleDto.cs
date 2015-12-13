namespace InstantDelivery.Model.Vehicles
{
    /// <summary>
    /// Obiekt DTO reprezentujący pojazd.
    /// </summary>
    public class VehicleDto
    {
        /// <summary>
        /// ID pojazdu
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Numer rejestracyjny
        /// </summary>
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// ID modelu pojazdu
        /// </summary>
        public int VehicleModelId { get; set; }

        /// <summary>
        /// Marka
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Model
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Ładowność
        /// </summary>
        public double Payload { get; set; }

        /// <summary>
        /// Dostępna ładowność X
        /// </summary>
        public double AvailableSpaceX { get; set; }

        /// <summary>
        /// Dostępna ładowność Y
        /// </summary>
        public double AvailableSpaceY { get; set; }

        /// <summary>
        /// Dostępna ładowność Z
        /// </summary>
        public double AvailableSpaceZ { get; set; }
    }
}
