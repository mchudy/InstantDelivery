using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using InstantDelivery.ViewModel.ViewModels.EmployeesViewModels;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel
{
    public class VehicleManageViewModel : EmployeesViewModelBase
    {
        private readonly IEmployeeService employeesService;
        private readonly IVehiclesService vehiclesService;
        private readonly IWindowManager windowManager;
        private Employee selectedEmployee;

        public VehicleManageViewModel(IEmployeeService employeesService, IWindowManager windowManager, IVehiclesService vehiclesService)
        {
            this.employeesService = employeesService;
            this.windowManager = windowManager;
            this.vehiclesService = vehiclesService;
            Employees = employeesService.GetAll();
        }

        public bool IsSelectedAnyRow
        {
            get { return SelectedEmployee != null; }
        }

        protected override IQueryable<Employee> GetEmployees()
        {
            return employeesService.GetAll();
        }

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                NotifyOfPropertyChange();
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsSelectedAnyRow)));
            }
        }

        public async void EditVehicleForEmployee()
        {
            if (SelectedEmployee == null)
            {
                return;
            }
            var result = windowManager.ShowDialog(new SelectVehicleForEmployeeViewModel(employeesService, vehiclesService)
            {
                SelectedEmployee = SelectedEmployee,
                SelectedVehicle = SelectedEmployee.Vehicle,
                Vehicles = vehiclesService.GetAllAvailableAndCurrent(SelectedEmployee.Vehicle),
                HasVehicle = SelectedEmployee.Vehicle != null
            });
            await Task.Run(() =>
            {
                if (result != true)
                {
                    employeesService.Reload(SelectedEmployee);
                }
                else
                {
                    employeesService.Save();
                }
            });
        }

    }
}