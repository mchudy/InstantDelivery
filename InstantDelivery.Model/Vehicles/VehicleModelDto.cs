namespace InstantDelivery.Model
{
    /// <summary>
    /// Obiekt DTO modelu pojazdu
    /// </summary>
    public class VehicleModelDto
    {
        /// <summary>
        /// ID modelu pojazdu
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Marka
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Model
        /// </summary>
        public string Model { get; set; }
    }
}