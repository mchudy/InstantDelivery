using Caliburn.Micro;
using InstantDelivery.Model;
using InstantDelivery.ViewModel.Proxies;
using PropertyChanged;
using System.Collections.Generic;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model widoku pojazdów używanych przez pracowników
    /// </summary>
    [ImplementPropertyChanged]
    public class EmployeesUsedVehiclesViewModel : EmployeesViewModelBase
    {
        private readonly EmployeesServiceProxy employeesService;
        private readonly IWindowManager windowManager;
        private EmployeeUsedVehiclesDetailsViewModel usedVehiclesDetailsViewModel;

        /// <summary>
        /// Konstruktor modelu widoku
        /// </summary>
        /// <param name="employeesService"></param>
        /// <param name="windowManager"></param>
        /// <param name="usedVehiclesDetailsViewModel"></param>
        public EmployeesUsedVehiclesViewModel(EmployeesServiceProxy employeesService, IWindowManager windowManager,
            EmployeeUsedVehiclesDetailsViewModel usedVehiclesDetailsViewModel)
        {
            this.employeesService = employeesService;
            this.windowManager = windowManager;
            this.usedVehiclesDetailsViewModel = usedVehiclesDetailsViewModel;
        }

        public new IList<EmployeeVehicleDto> Employees { get; set; }

        /// <summary>
        /// Aktualnie zaznaczony wiersz w tabeli danych.
        /// </summary>
        public EmployeeVehicleDto SelectedRow { get; set; }

        /// <summary>
        /// Flaga informująca o tym czy aktualnie zaznaczony jest jakiś wiersz w tabeli danych.
        /// </summary>
        public bool IsEnabledViewDetails => SelectedRow != null;

        /// <summary>
        /// Delegat zdarzenia przejścia do widoku szczegółów pojazdu używanego przez zaznaczonego pracownika.
        /// </summary>
        public void ShowVehicleDetails()
        {
            if (SelectedRow == null)
            {
                return;
            }
            usedVehiclesDetailsViewModel.Vehicle = SelectedRow.Vehicle;
            windowManager.ShowDialog(usedVehiclesDetailsViewModel);
        }

        protected override async void UpdateData()
        {
            var query = GetPageQuery();
            AddFilters(query);
            var pageDto = await employeesService.VehiclesPage(query);
            PageCount = pageDto.PageCount;
            Employees = pageDto.PageCollection;
        }
    }
}