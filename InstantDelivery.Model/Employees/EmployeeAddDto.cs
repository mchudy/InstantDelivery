using System.ComponentModel.DataAnnotations;
using InstantDelivery.Domain.Entities;

namespace InstantDelivery.Model
{
    public class EmployeeAddDto : ValidationBase
    {
        [RegularExpression("[A-ZĄĆĘŁŃÓŚŹŻ]{1}[a-ząćęłńóśżź]+", ErrorMessage = "Proszę podać poprawne imię")]
        public string MotherName { get; set; }
        [RegularExpression("[A-ZĄĆĘŁŃÓŚŹŻ]{1}[a-ząćęłńóśżź]+", ErrorMessage = "Proszę podać poprawne imię")]
        public string FatherName { get; set; }
    }
}
