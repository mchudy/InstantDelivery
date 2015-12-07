using InstantDelivery.Common.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using InstantDelivery.Domain.Entities;
namespace InstantDelivery.Model
{
    /// <summary>
    /// Obiekt DTO zawierający dane osobowe pracownika
    /// </summary
    public class EmployeeDto : ValidationBase
    {
        /// <summary>
        /// Id pracownika
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Imię
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression("[A-ZĄĆĘŁŃÓŚŹŻ]{1}[a-ząćęłńóśżź]*", ErrorMessage = "Proszę podać poprawne imię")]
        public string FirstName { get; set; }

        /// <summary>
        /// Płeć
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [Required(ErrorMessage = "To pole jest wymagane")]
        public Gender Gender { get; set; }

        /// <summary>
        /// Nazwisko
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression("[A-ZĄĆĘŁŃÓŚŹŻ]{1}[a-ząćęłńóśżź]*", ErrorMessage = "Proszę podać poprawne nazwisko")]
        public string LastName { get; set; }

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
        /// Pensja
        /// </summary>
        public decimal Salary { get; set; }

        /// <summary>
        /// Data zatrudnienia
        /// </summary>
        public DateTime? HireDate { get; set; }
    }
}
