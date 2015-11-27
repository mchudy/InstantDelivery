namespace InstantDelivery.Model
{
    public class PackageDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Waga paczki
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// Wysokość paczki
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// Szerokość paczki
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        /// Długość paczki
        /// </summary>
        public double Length { get; set; }
        /// <summary>
        /// Koszt paczki
        /// </summary>
        public decimal Cost { get; set; }
    }
}
