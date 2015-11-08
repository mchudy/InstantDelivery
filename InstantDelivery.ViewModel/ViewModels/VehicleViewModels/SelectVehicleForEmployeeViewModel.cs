using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using PropertyChanged;
using System.Linq;

namespace InstantDelivery.ViewModel
{
    [ImplementPropertyChanged]
    public class SelectVehicleForEmployeeViewModel : Screen
    {
        public IEmployeeService employeeService;
        public IVehiclesService vehicleService;

        public SelectVehicleForEmployeeViewModel(IEmployeeService employeeService, IVehiclesService vehicleService)
        {
            this.employeeService = employeeService;
            this.vehicleService = vehicleService;
        }

        public bool HasVehicle { get; set; }

        public Employee SelectedEmployee { get; set; }

        public Vehicle SelectedVehicle { get; set; }

        public IQueryable<Vehicle> Vehicles { get; set; }

        public void Save()
        {
            if (!HasVehicle)
            {
                SelectedVehicle = null;
            }
            employeeService.ChangeEmployeesVehicle(SelectedEmployee, SelectedVehicle);
            TryClose(true);
        }

        public void Cancel()
        {
            TryClose(false);
        }
    }
}