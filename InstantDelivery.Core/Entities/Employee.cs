using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InstantDelivery.Core.Entities
{
    /// <summary>
    /// Klasa reprezentująca pracownika.
    /// </summary>
    [ImplementPropertyChanged]
    public class Employee : ValidationBase
    {
        /// <summary>
        /// Imię
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression("[A-ZĄĆĘŁŃÓŚŹŻ]{1}[a-ząćęłńóśżź]*", ErrorMessage = "Proszę podać poprawne imię")]
        public string FirstName { get; set; }

        /// <summary>
        /// Nazwisko
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression("[A-ZĄĆĘŁŃÓŚŹŻ]{1}[a-ząćęłńóśżź]*", ErrorMessage = "Proszę podać poprawne nazwisko")]
        public string LastName { get; set; }
        /// <summary>
        /// Płeć
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        public Gender Gender { get; set; }
        /// <summary>
        /// Data urodzenia
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Numer telefonu
        /// </summary>
        [Phone(ErrorMessage = "Proszę podać poprawny numer telefonu")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Adres zamieszkania
        /// </summary>
        public Address PlaceOfResidence { get; set; } = new Address();
        /// <summary>
        /// Adres email
        /// </summary>
        [EmailAddress(ErrorMessage = "Proszę podać poprawny email")]
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
        [RegularExpression("[A-ZĄĆĘŁŃÓŚŹŻ]{1}[a-ząćęłńóśżź]+", ErrorMessage = "Proszę podać poprawne imię")]
        public string MotherName { get; set; }
        /// <summary>
        /// Imię ojca
        /// </summary>
        [RegularExpression("[A-ZĄĆĘŁŃÓŚŹŻ]{1}[a-ząćęłńóśżź]+", ErrorMessage = "Proszę podać poprawne imię")]
        public string FatherName { get; set; }
        /// <summary>
        /// Pensja
        /// </summary>
        [Range(0.0, int.MaxValue)]
        [Required(ErrorMessage = "To pole jest wymagane")]
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
    }
    /// <summary>
    /// Definicja reprezentacji płci
    /// </summary>
    public enum Gender
    {
        [Description("Mężczyczna")]
        Male,
        [Description("Kobieta")]
        Female
    };
}