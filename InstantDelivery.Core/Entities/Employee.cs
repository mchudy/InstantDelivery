using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace InstantDelivery.Core.Entities
{
    public enum Gender
    {
        [Description("Mężczyzna")]
        Male,
        [Description("Kobieta")]
        Female
    };

    [ImplementPropertyChanged]
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public Address PlaceOfResidence { get; set; }
        public string Email { get; set; }
        public string Pesel { get; set; }
        public string Comments { get; set; }
        //public string FamilyName { get; set; }
        public string MotherMaidenName { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public decimal Salary { get; set; }
        public DateTime? HireDate { get; set; }
        public string Citizenship { get; set; }
        public virtual ICollection<Package> Packages { get; set; }

    }
}