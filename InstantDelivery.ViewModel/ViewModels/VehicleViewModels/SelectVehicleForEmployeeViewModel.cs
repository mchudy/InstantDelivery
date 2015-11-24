using InstantDelivery.Domain.Entities;
using InstantDelivery.Services;
using InstantDelivery.Services.Paging;
using PropertyChanged;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    ///  Model widoku wyboru pojazdu dla pracownika.
    /// </summary>
    [ImplementPropertyChanged]
    public class SelectVehicleForEmployeeViewModel : PagingViewModel
    {
        private readonly IEmployeeService employeeService;
        private readonly IVehiclesService vehiclesService;

        /// <summary>
        /// Konstruktor modelu widoku
        /// </summary>
        /// <param name="employeeService"></param>
        /// <param name="vehiclesService"></param>
        public SelectVehicleForEmployeeViewModel(IEmployeeService employeeService,
            IVehiclesService vehiclesService)
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
        public Employee SelectedEmployee { get; set; }

        /// <summary>
        /// Zaznaczony wiersz w widoku.
        /// </summary>
        public Vehicle SelectedVehicle { get; set; }

        /// <summary>
        /// Kolekcja skojarzona z tabelą danych w widoku.
        /// </summary>
        public IList<Vehicle> Vehicles { get; set; }

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

        protected override void UpdateData()
        {
            var query = new PageQuery<Vehicle>
            {
                PageSize = PageSize,
                PageIndex = CurrentPage,
                SortDirection = SortDirection,
                SortProperty = SortProperty
            };
            var pageDto = vehiclesService.GetAllAvailableAndCurrent(SelectedVehicle, query);
            PageCount = pageDto.PageCount;
            Vehicles = pageDto.PageCollection;
        }
    }
}