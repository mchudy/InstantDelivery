using Caliburn.Micro;
using InstantDelivery.Model;
using InstantDelivery.ViewModel.Proxies;
using System.ComponentModel;
using InstantDelivery.Model.Employees;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    ///  Model widoku zarządzania pojazdami.
    /// </summary>
    public class VehicleManageViewModel : EmployeesViewModelBase
    {
        private readonly EmployeesServiceProxy employeesService;
        private readonly SelectVehicleForEmployeeViewModel selectVehicleViewModel;
        private readonly IWindowManager windowManager;
        private EmployeeVehicleDto selectedEmployee;

        /// <summary>
        /// Konstruktor modelu widoku
        /// </summary>
        /// <param name="employeesService"></param>
        /// <param name="windowManager"></param>
        /// <param name="vehiclesService"></param>
        /// <param name="selectVehicleViewModel"></param>
        public VehicleManageViewModel(EmployeesServiceProxy employeesService, IWindowManager windowManager,
            SelectVehicleForEmployeeViewModel selectVehicleViewModel)
            : base(employeesService)
        {
            this.employeesService = employeesService;
            this.windowManager = windowManager;
            this.selectVehicleViewModel = selectVehicleViewModel;
        }

        /// <summary>
        /// Flaga informująca o tym czy zaznaczony jest jakiś wiersz.
        /// </summary>
        public bool IsSelectedAnyRow => SelectedEmployee != null;

        public new BindableCollection<EmployeeVehicleDto> Employees { get; set; }

        /// <summary>
        /// Aktualnie zaznaczony wiersz w tabeli danych.
        /// </summary>
        public EmployeeVehicleDto SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                NotifyOfPropertyChange();
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsSelectedAnyRow)));
            }
        }

        /// <summary>
        /// Delegat zdarzenia kliknięcia w przycisk przechodzący do widoku edycji pojazdu.
        /// </summary>
        public void EditVehicleForEmployee()
        {
            if (SelectedEmployee == null)
            {
                return;
            }
            selectVehicleViewModel.SelectedEmployee = SelectedEmployee;
            selectVehicleViewModel.SelectedVehicle = SelectedEmployee.Vehicle;
            selectVehicleViewModel.HasVehicle = SelectedEmployee.Vehicle != null;
            var result = windowManager.ShowDialog(selectVehicleViewModel);
            if (result == true)
            {
                UpdateData();
            }
        }

        protected override async void UpdateData()
        {
            var query = GetPageQuery();
            AddFilters(query);
            var pageDto = await employeesService.VehiclesPage(query);
            PageCount = pageDto.PageCount;
            Employees = new BindableCollection<EmployeeVehicleDto>(pageDto.PageCollection);
        }
    }
}