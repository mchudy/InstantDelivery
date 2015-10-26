using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Core.Services;

namespace InstantDelivery.ViewModel
{
    public class EmployeesViewModel : Screen
    {
        private IQueryable<Employee> employees;
        private List<Employee> employeesList = new List<Employee>(); 
        public List<Employee> Employees
        {
            get
            {
                employees= new EmployeeServices().Employees;
                employeesList= new EmployeeServices().EmployeesList;
                return employeesList;
            }
            set
            {
                OnPropertyChanged(null);
            }
        }

        public List<Foo> Ala { get; set; }=new List<Foo>() {new Foo(), new Foo(), new Foo()}; 
    }

    public class Foo
    {
        public string A { get; set; } = "A";
        public string B { get; set; } = "B";
    }
}