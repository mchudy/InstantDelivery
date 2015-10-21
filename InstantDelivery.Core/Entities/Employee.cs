using System;

namespace InstantDelivery.Core.Entities
{
    public enum Sex
    {
        Man,
        Woman
    };

    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Sex Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }

        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
    }
}