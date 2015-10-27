using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Core.Services;
using System.Collections.Generic;

namespace InstantDelivery.ViewModel
{
    public class EmployeesViewModel : Screen
    {
        private EmployeesRepository repository;

        public EmployeesViewModel(EmployeesRepository repository)
        {
            this.repository = repository;
            Employees = repository.GetAll();
        }

        public IList<Employee> Employees { get; set; }
    }
}