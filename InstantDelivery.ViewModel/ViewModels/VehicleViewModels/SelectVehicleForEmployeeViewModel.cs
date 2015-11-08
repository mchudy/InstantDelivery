using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using PropertyChanged;
using System.Linq;
using System.Threading.Tasks;
using Action = System.Action;

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

        public async void Save()
        {
            if (!HasVehicle)
            {
                SelectedVehicle = null;
            }
            var VehicleToSave = SelectedVehicle;
            var EmployeeToUpdate = SelectedEmployee;
            TryClose(true);
            await Task.Run(() =>
            {
                employeeService.ChangeEmployeesVehicle(EmployeeToUpdate, VehicleToSave);
                
            });
        }

        public void Cancel()
        {
            TryClose(false);
        }
    }
}