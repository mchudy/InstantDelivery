using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Core.Repositories;
using System.Collections.ObjectModel;

namespace InstantDelivery.ViewModel
{
    public class EmployeesViewModel : Screen
    {
        private readonly EmployeesRepository repository;
        private readonly IWindowManager windowManager;
        private Employee selectedEmployee;

        public EmployeesViewModel(EmployeesRepository repository, IWindowManager windowManager)
        {
            this.repository = repository;
            this.windowManager = windowManager;
            Employees = new ObservableCollection<Employee>(repository.GetAll());
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

        public ObservableCollection<Employee> Employees { get; set; }

        public void EditEmployee()
        {
            bool? result = windowManager.ShowDialog(new EmployeeEditViewModel
            {
                Employee = SelectedEmployee
            });
            if (result != true)
            {
                repository.Reload(SelectedEmployee);
            }
            else
            {
                repository.Save();
            }
        }
    }
}