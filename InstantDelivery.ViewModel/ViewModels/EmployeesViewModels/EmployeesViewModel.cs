using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using InstantDelivery.ViewModel.ViewModels.EmployeesViewModels;
using System.Linq;

namespace InstantDelivery.ViewModel
{
    public class EmployeesViewModel : EmployeesViewModelBase
    {
        private readonly EmployeeService repository;
        private readonly IWindowManager windowManager;
        private Employee selectedEmployee;

        public EmployeesViewModel(EmployeeService repository, IWindowManager windowManager)
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
                NotifyOfPropertyChange(() => IsSelectedAnyRow);
            }
        }

        public bool IsSelectedAnyRow => SelectedEmployee != null;

        public void EditEmployee()
        {
            if (SelectedEmployee == null)
            {
                return;
            }
            var result = windowManager.ShowDialog(new EmployeeEditViewModel
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

        public void RemoveEmployee()
        {
            if (SelectedEmployee == null)
            {
                return;
            }
            var result = windowManager.ShowDialog(new ConfirmDeleteViewModel());
            if (result == true)
            {
                repository.RemoveEmployee(SelectedEmployee);
            }
        }

        protected override IQueryable<Employee> GetEmployees()
        {
            return repository.GetAll();
        }
    }
}