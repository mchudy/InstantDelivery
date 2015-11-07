using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using InstantDelivery.ViewModel.ViewModels.EmployeesViewModels;
using System.Linq;

namespace InstantDelivery.ViewModel
{
    public class EmployeesViewModel : EmployeesViewModelBase
    {
        private readonly IEmployeeService employeeService;
        private readonly IWindowManager windowManager;
        private Employee selectedEmployee;
        private EmployeeEditViewModel employeeEditViewModel;
        private ConfirmDeleteViewModel confirmDeleteViewModel;

        public EmployeesViewModel(IEmployeeService employeeService, IWindowManager windowManager,
            EmployeeEditViewModel employeeEditViewModel, ConfirmDeleteViewModel confirmDeleteViewModel)
        {
            this.employeeService = employeeService;
            this.windowManager = windowManager;
            this.employeeEditViewModel = employeeEditViewModel;
            this.confirmDeleteViewModel = confirmDeleteViewModel;
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
            employeeEditViewModel.Employee = SelectedEmployee;
            var result = windowManager.ShowDialog(employeeEditViewModel);
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
            var result = windowManager.ShowDialog(confirmDeleteViewModel);
            if (result == true)
            {
                employeeService.RemoveEmployee(SelectedEmployee);
                CurrentPage = CurrentPage;
            }
        }

        protected override IQueryable<Employee> GetEmployees()
        {
            return employeeService.GetAll();
        }
    }
}