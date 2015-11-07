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
        private EmployeeUsedVehiclesDetailsViewModel usedVehiclesDetailsViewModel;

        public EmployeesUsedVehiclesViewModel(IEmployeeService employeeService, IWindowManager windowManager,
            EmployeeUsedVehiclesDetailsViewModel usedVehiclesDetailsViewModel)
        {
            this.employeeService = employeeService;
            this.windowManager = windowManager;
            this.usedVehiclesDetailsViewModel = usedVehiclesDetailsViewModel;
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
            usedVehiclesDetailsViewModel.Employee = SelectedRow;
            var result = windowManager.ShowDialog(usedVehiclesDetailsViewModel);
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