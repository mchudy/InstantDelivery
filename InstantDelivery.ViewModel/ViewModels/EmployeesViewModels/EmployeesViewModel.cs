using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using InstantDelivery.ViewModel.ViewModels.EmployeesViewModels;
using System.Linq;

namespace InstantDelivery.ViewModel
{
    public class EmployeesViewModel : EmployeesViewModelBase
    {
        private readonly EmployeeService employeeService;
        private readonly IWindowManager windowManager;
        private Employee selectedEmployee;

        public EmployeesViewModel(EmployeeService employeeService, IWindowManager windowManager)
        {
            this.employeeService = employeeService;
            this.windowManager = windowManager;
            Employees = employeeService.GetAll();
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
                employeeService.Reload(SelectedEmployee);
            }
            else
            {
                employeeService.Save();
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
                employeeService.RemoveEmployee(SelectedEmployee);
            }
        }

        protected override IQueryable<Employee> GetEmployees()
        {
            return employeeService.GetAll();
        }
    }
}