using System.ComponentModel.DataAnnotations;
using InstantDelivery.Domain.Entities;

namespace InstantDelivery.Model
{
    public class EmployeeVehicleDto : ValidationBase
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
        /// Nazwisko
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression("[A-ZĄĆĘŁŃÓŚŹŻ]{1}[a-ząćęłńóśżź]*", ErrorMessage = "Proszę podać poprawne nazwisko")]
        public string LastName { get; set; }

        public VehicleDto Vehicle { get; set; }
    }
}
