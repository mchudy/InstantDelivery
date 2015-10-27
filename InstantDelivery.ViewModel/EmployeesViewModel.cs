using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Core.Repositories;
using System.Collections.Generic;

namespace InstantDelivery.ViewModel
{
    public class EmployeesViewModel : Screen
    {
        private EmployeesRepository repository;
        private IWindowManager windowManager;
        private Employee selectedEmployee;

        public EmployeesViewModel(EmployeesRepository repository, IWindowManager windowManager)
        {
            this.repository = repository;
            this.windowManager = windowManager;
            Employees = repository.GetAll();
        }

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                NotifyOfPropertyChange();
            }
        }

        public IList<Employee> Employees { get; set; }

        public void EditEmployee()
        {
            windowManager.ShowDialog(new EmployeeEditViewModel()
            {
                Employee = SelectedEmployee
            });
        }
    }
}