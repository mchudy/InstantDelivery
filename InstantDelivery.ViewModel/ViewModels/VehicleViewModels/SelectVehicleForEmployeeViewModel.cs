using InstantDelivery.Model;
using InstantDelivery.ViewModel.Proxies;
using PropertyChanged;
using System.Collections.Generic;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    ///  Model widoku wyboru pojazdu dla pracownika.
    /// </summary>
    [ImplementPropertyChanged]
    public class SelectVehicleForEmployeeViewModel : PagingViewModel
    {
        private readonly EmployeesServiceProxy employeeService;
        private readonly VehiclesServiceProxy vehiclesService;

        /// <summary>
        /// Konstruktor modelu widoku
        /// </summary>
        /// <param name="employeeService"></param>
        /// <param name="vehiclesService"></param>
        public SelectVehicleForEmployeeViewModel(EmployeesServiceProxy employeeService,
            VehiclesServiceProxy vehiclesService)
        {
            this.employeeService = employeeService;
            this.vehiclesService = vehiclesService;
        }

        /// <summary>
        /// Flaga informująca o tym czy pracownik ma przypisany samochód.
        /// </summary>
        public bool HasVehicle { get; set; }

        /// <summary>
        /// Zaznaczony pracownik w poprzednim widoku.
        /// </summary>
        public EmployeeVehicleDto SelectedEmployee { get; set; }

        /// <summary>
        /// Zaznaczony wiersz w widoku.
        /// </summary>
        public VehicleDto SelectedVehicle { get; set; }

        /// <summary>
        /// Kolekcja skojarzona z tabelą danych w widoku.
        /// </summary>
        public IList<VehicleDto> Vehicles { get; set; }

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
            await employeeService.ChangeVehicle(EmployeeToUpdate.Id, VehicleToSave?.Id);
            TryClose(true);
        }

        /// <summary>
        /// Anuluje zmiany dokonane w widoku.
        /// </summary>
        public void Cancel()
        {
            TryClose(false);
        }

        protected override async void UpdateData()
        {
            var query = GetPageQuery();
            var pageDto = await vehiclesService.AvailableVehiclesPage(query);
            PageCount = pageDto.PageCount;
            Vehicles = pageDto.PageCollection;
            if (SelectedEmployee.Vehicle != null)
            {
                Vehicles.Add(SelectedEmployee.Vehicle);
                SelectedVehicle = SelectedEmployee?.Vehicle;
            }
        }
    }
}