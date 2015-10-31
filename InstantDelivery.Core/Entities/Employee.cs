using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InstantDelivery.Core.Entities
{
    public enum Gender
    {
        Male,
        Female
    };

    [ImplementPropertyChanged]
    public class Employee : ValidationBase
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression("[A-Z]{1}[a-z]+", ErrorMessage = "Proszę podać poprawne imię")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression("[A-Z]{1}[a-z]+", ErrorMessage = "Proszę podać poprawne nazwisko")]
        public string LastName { get; set; }

        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }
        public string Pesel { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Comments { get; set; }
        public string FamilyName { get; set; }
        public string MotherMaidenName { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }

        [Range(0.0, int.MaxValue)]
        public decimal Salary { get; set; }
        public DateTime? HireDate { get; set; }
        public string Citizenship { get; set; }
        public virtual ICollection<Package> Packages { get; set; }
    }
}