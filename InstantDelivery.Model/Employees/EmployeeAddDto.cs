using System.ComponentModel.DataAnnotations;

namespace InstantDelivery.Model.Employees
{
    /// <summary>
    /// Obiekt DTO zawierający dane pracownika który będzie dodany do bazy danych.
    /// </summary>
    public class EmployeeAddDto : EmployeeDto
    {
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
    }
}
