using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using InstantDelivery.ViewModel.ViewModels.EmployeesViewModels;
using System.Linq;

namespace InstantDelivery.ViewModel
{
    public class EmployeesUsedVehiclesViewModel : EmployeesViewModelBase
    {
        private readonly IEmployeeService employeeService;
        private readonly IWindowManager windowManager;
        private Employee selectedRow;

        public EmployeesUsedVehiclesViewModel(IEmployeeService employeeService, IWindowManager windowManager)
        {
            this.employeeService = employeeService;
            this.windowManager = windowManager;
            Employees = employeeService.GetAll();
        }

        public Employee SelectedRow
        {
            get { return selectedRow; }
            set
            {
                selectedRow = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => IsEnabledViewDetails);
            }
        }

        public bool IsEnabledViewDetails => SelectedRow != null;

        public void ShowVehicleDetails()
        {
            if (SelectedRow == null)
            {
                return;
            }
            var result = windowManager.ShowDialog(new EmployeeUsedVehiclesDetailsViewModel
            {
                SelectedRow = SelectedRow
            });
            if (result != true)
            {
                employeeService.Reload(SelectedRow);
            }
            else
            {
                employeeService.Save();
            }
        }

        protected override IQueryable<Employee> GetEmployees()
        {
            return employeeService.GetAll();
        }
    }
}