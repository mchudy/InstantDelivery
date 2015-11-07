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
        // to vehicles sie nie binduje, help :<
        public SelectVehicleForEmployeeViewModel(IEmployeeService employeeService,IVehiclesService vehicleService)
        {
            this.employeeService = employeeService;
            this.vehicleService = vehicleService;
            Vehicles = vehicleService.GetAll();//AvailableAndCurrent(SelectedVehicle);
        }

        public void ChangeVehicleForEmployee()
        {
            SelectedEmployee.Vehicle = SelectedVehicle;
        }

        public void Save()
        {
            ChangeVehicleForEmployee();
            TryClose(true);
        }

        public void Cancel()
        {
            TryClose(false);
        }
    }
}