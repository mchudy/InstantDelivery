using InstantDelivery.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InstantDelivery.Domain.Entities
{
    /// <summary>
    /// Klasa reprezentująca pracownika.
    /// </summary>
    public class Employee : Entity
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
        /// Komentarz
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Imię matki
        /// </summary>
        public string MotherName { get; set; }

        /// <summary>
        /// Imię ojca
        /// </summary>
        public string FatherName { get; set; }

        /// <summary>
        /// Pensja
        /// </summary>
        public decimal Salary { get; set; }

        /// <summary>
        /// Data zatrudnienia
        /// </summary>
        public DateTime? HireDate { get; set; }

        /// <summary>
        /// Paczki pracownika
        /// </summary>
        public virtual ICollection<Package> Packages { get; set; } = new HashSet<Package>();

        /// <summary>
        /// Samochód pracownika
        /// </summary>
        public virtual Vehicle Vehicle { get; set; }

        public virtual User User { get; set; }
    }

}