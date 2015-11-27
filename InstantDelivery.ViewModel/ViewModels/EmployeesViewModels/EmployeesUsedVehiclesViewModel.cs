using Caliburn.Micro;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Services;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model widoku pojazdów używanych przez pracowników
    /// </summary>
    public class EmployeesUsedVehiclesViewModel : EmployeesViewModelBase
    {
        private readonly IEmployeeService employeesService;
        private readonly IWindowManager windowManager;
        private Employee selectedRow;
        private EmployeeUsedVehiclesDetailsViewModel usedVehiclesDetailsViewModel;

        /// <summary>
        /// Konstruktor modelu widoku
        /// </summary>
        /// <param name="employeesService"></param>
        /// <param name="windowManager"></param>
        /// <param name="usedVehiclesDetailsViewModel"></param>
        public EmployeesUsedVehiclesViewModel(IEmployeeService employeesService, IWindowManager windowManager,
            EmployeeUsedVehiclesDetailsViewModel usedVehiclesDetailsViewModel)
        {
            this.employeesService = employeesService;
            this.windowManager = windowManager;
            this.usedVehiclesDetailsViewModel = usedVehiclesDetailsViewModel;
        }

        /// <summary>
        /// Aktualnie zaznaczony wiersz w tabeli danych.
        /// </summary>
        public Employee SelectedRow
        {
            get { return selectedRow; }
            set
            {
                selectedRow = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => IsEnabledViewDetails);
            }
        }

        /// <summary>
        /// Flaga informująca o tym czy aktualnie zaznaczony jest jakiś wiersz w tabeli danych.
        /// </summary>
        public bool IsEnabledViewDetails => SelectedRow != null;

        /// <summary>
        /// Delegat zdarzenia przejścia do widoku szczegółów pojazdu używanego przez zaznaczonego pracownika.
        /// </summary>
        public async void ShowVehicleDetails()
        {
            if (SelectedRow == null)
            {
                return;
            }
            usedVehiclesDetailsViewModel.Employee = SelectedRow;
            var result = windowManager.ShowDialog(usedVehiclesDetailsViewModel);
            await Task.Run(() =>
            {
                if (result != true)
                {
                    employeesService.Reload(SelectedRow);
                }
                else
                {
                    employeesService.Save();
                }
            });
        }
    }
}