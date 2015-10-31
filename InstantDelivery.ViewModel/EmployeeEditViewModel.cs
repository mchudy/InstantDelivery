using System;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using InstantDelivery.Core.Entities;

namespace InstantDelivery.ViewModel
{
    public class EmployeeEditViewModel : Screen
    {
        public Employee Employee { get; set; }

        public decimal Salary
        {
            get { return Employee.Salary; }
            set { Employee.Salary = value; }
        }

        public string FirstName
        {
            get { return Employee.FirstName; }
            set { Employee.FirstName = value; }
        }

        public string LastName
        {
            get { return Employee.LastName; }
            set { Employee.LastName = value; }
        }

        public string PhoneNumber
        {
            get { return Employee.PhoneNumber; }
            set { Employee.PhoneNumber = value; }
        }

        public void Save()
        {
            TryClose(true);
        }

        public void Cancel()
        {
            TryClose(false);
        }
    }
}
