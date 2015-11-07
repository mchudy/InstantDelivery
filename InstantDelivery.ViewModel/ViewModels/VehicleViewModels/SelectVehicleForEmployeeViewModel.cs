using System.Linq;
using Caliburn.Micro;
using InstantDelivery.Core;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;

namespace InstantDelivery.ViewModel
{
    public class SelectVehicleForEmployeeViewModel : Screen
    {
        public IEmployeeService employeeService;
        public IVehiclesService vehicleService;
        private Employee selectedEmployee;

        public Employee SelectedEmployee
        {
            get
            {
                return selectedEmployee;
            }
            set
            {
                selectedEmployee = value;
                NotifyOfPropertyChange();
            }
        }

        private Vehicle selectedVehicle;

        public Vehicle SelectedVehicle
        {
            get
            {
                return selectedVehicle;
            }
            set
            {
                selectedVehicle = value;
                NotifyOfPropertyChange();
            }
        }

        private IQueryable<Vehicle> vehicles;

        public IQueryable<Vehicle> Vehicles
        {
            get { return vehicles; }
            set
            {
                vehicles = value;
                NotifyOfPropertyChange();
            }
        }

        // chce tu wstrzyknąć SelectedEmployee (z poprzedniego widoku czyli z VehicleManageViewModel z zaznaczonego na gridzie elementu)
        public SelectVehicleForEmployeeViewModel(IEmployeeService employeeService,IVehiclesService vehicleService)
        {
            this.employeeService = employeeService;
            this.vehicleService = vehicleService;
        }

        public void ChangeVehicleForEmployee()
        {
            employeeService.ChangeEmployeesVehicle(SelectedEmployee, SelectedVehicle);
        }

        public void Save()
        {
            TryClose(true);
        }

        public void Cancel()
        {
            TryClose(false);
        }
    }
}