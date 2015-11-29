using System;

namespace InstantDelivery.Model
{
    /// <summary>
    /// Obiekt DTO zawierający dane osobowe pracownika
    /// </summary>
    public class EmployeeDto
    {
        /// <summary>
        /// Id pracownika
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Imię
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Nazwisko
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Data urodzenia
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Numer telefonu
        /// </summary>
        public string PhoneNumber { get; set; }

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
        /// Pensja
        /// </summary>
        public decimal Salary { get; set; }

        /// <summary>
        /// Data zatrudnienia
        /// </summary>
        public DateTime? HireDate { get; set; }
    }
}
