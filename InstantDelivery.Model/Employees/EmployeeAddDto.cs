using System.ComponentModel.DataAnnotations;

namespace InstantDelivery.Model.Employees
{
    public class EmployeeAddDto : EmployeeDto
    {
        [RegularExpression("[A-ZĄĆĘŁŃÓŚŹŻ]{1}[a-ząćęłńóśżź]+", ErrorMessage = "Proszę podać poprawne imię")]
        public string MotherName { get; set; }
        [RegularExpression("[A-ZĄĆĘŁŃÓŚŹŻ]{1}[a-ząćęłńóśżź]+", ErrorMessage = "Proszę podać poprawne imię")]
        public string FatherName { get; set; }
    }
}
