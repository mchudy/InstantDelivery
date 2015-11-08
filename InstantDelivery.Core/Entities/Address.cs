using System.ComponentModel.DataAnnotations.Schema;

namespace InstantDelivery.Core.Entities
{
    /// <summary>
    /// Klasa reprezentująca adres.
    /// </summary>
    [ComplexType]
    public class Address
    {
        /// <summary>
        /// Miasto
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Ulica
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// Numer domu
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// Kod pocztowy
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// Województwo
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// Kraj
        /// </summary>
        public string Country { get; set; }
    }
}