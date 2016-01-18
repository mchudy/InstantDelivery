using InstantDelivery.Common.Enums;

namespace InstantDelivery.Domain.Entities
{
    /// <summary>
    /// Klasa reprezentująca paczkę
    /// </summary>
    public class Package : Entity
    {
        /// <summary>
        /// Adres dostawy paczki
        /// </summary>
        public Address ShippingAddress { get; set; } = new Address();

        /// <summary>
        /// Nadawca
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// Odbiorca
        /// </summary>
        public string Recipient { get; set; }

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

        /// <summary>
        /// Status paczki
        /// </summary>
        public PackageStatus Status { get; set; }
    }
}