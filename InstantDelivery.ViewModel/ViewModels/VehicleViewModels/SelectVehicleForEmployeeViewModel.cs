using Caliburn.Micro;
using InstantDelivery.Services;
using PropertyChanged;
using System.Linq;
using System.Threading.Tasks;
using InstantDelivery.Domain.Entities;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    ///  Model widoku wyboru pojazdu dla pracownika.
    /// </summary>
    [ImplementPropertyChanged]
    public class SelectVehicleForEmployeeViewModel : Screen
    {
        private IEmployeeService employeeService;

        /// <summary>
        /// Konstruktor modelu widoku
        /// </summary>
        /// <param name="employeeService"></param>
        /// <param name="vehicleService"></param>
        public SelectVehicleForEmployeeViewModel(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        /// <summary>
        /// Flaga informująca o tym czy pracownik ma przypisany samochód.
        /// </summary>
        public bool HasVehicle { get; set; }

        /// <summary>
        /// Zaznaczony pracownik w poprzednim widoku.
        /// </summary>
        public Employee SelectedEmployee { get; set; }

        /// <summary>
        /// Zaznaczony wiersz w widoku.
        /// </summary>
        public Vehicle SelectedVehicle { get; set; }

        /// <summary>
        /// Kolekcja skojarzona z tabelą danych w widoku.
        /// </summary>
        public IQueryable<Vehicle> Vehicles { get; set; }

        /// <summary>
        /// Zapisuje zmiany dokonane w widoku.
        /// </summary>
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

        /// <summary>
        /// Anuluje zmiany dokonane w widoku.
        /// </summary>
        public void Cancel()
        {
            TryClose(false);
        }
    }
}