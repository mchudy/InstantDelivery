using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InstantDelivery.Common.Enums;

namespace InstantDelivery.Domain.Entities
{
    public class Customer : Entity
    {
        /// <summary>
        /// Imię
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Nazwisko
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Płeć
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Data urodzenia
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Numer telefonu
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Adres zamieszkania
        /// </summary>
        public Address PlaceOfResidence { get; set; } = new Address();

        /// <summary>
        /// Adres email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Numer PESEL
        /// </summary>
        public string Pesel { get; set; }

        /// <summary>
        /// Paczki klienta
        /// </summary>
        public virtual ICollection<Package> Packages { get; set; } = new HashSet<Package>();

        /// <summary>
        /// Relacja pomiędzy klientem a użytkownikiem
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Ranga klienta
        /// </summary>
        public Rank Rank { get; set; }
    }
}