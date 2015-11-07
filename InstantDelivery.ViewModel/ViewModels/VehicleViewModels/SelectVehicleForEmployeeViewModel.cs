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
        public Employee SelectedEmployee { get; set; }

        public Vehicle SelectedVehicle { get; set; }

        public IObservableCollection<Vehicle> Vehicles;

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