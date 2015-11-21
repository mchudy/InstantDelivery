using Caliburn.Micro;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Services;
using InstantDelivery.ViewModel.ViewModels.EmployeesViewModels;
using System.ComponentModel;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    ///  Model widoku zarządzania pojazdami.
    /// </summary>
    public class VehicleManageViewModel : EmployeesViewModelBase
    {
        private readonly IEmployeeService employeesesEmployeesService;
        private readonly IVehiclesService vehiclesService;
        private readonly IWindowManager windowManager;
        private Employee selectedEmployee;

        /// <summary>
        /// Konstruktor modelu widoku
        /// </summary>
        /// <param name="employeesesEmployeesService"></param>
        /// <param name="windowManager"></param>
        /// <param name="vehiclesService"></param>
        public VehicleManageViewModel(IEmployeeService employeesesEmployeesService, IWindowManager windowManager,
            IVehiclesService vehiclesService)
            : base(employeesesEmployeesService)
        {
            this.employeesesEmployeesService = employeesesEmployeesService;
            this.windowManager = windowManager;
            this.vehiclesService = vehiclesService;
        }

        /// <summary>
        /// Flaga informująca o tym czy zaznaczony jest jakiś wiersz.
        /// </summary>
        public bool IsSelectedAnyRow
        {
            get { return SelectedEmployee != null; }
        }

        /// <summary>
        /// Aktualnie zaznaczony wiersz w tabeli danych.
        /// </summary>
        public Employee SelectedEmployee
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
        public async void EditVehicleForEmployee()
        {
            if (SelectedEmployee == null)
            {
                return;
            }
            var result = windowManager.ShowDialog(new SelectVehicleForEmployeeViewModel(employeesesEmployeesService)
            {
                SelectedEmployee = SelectedEmployee,
                SelectedVehicle = SelectedEmployee.Vehicle,
                Vehicles = vehiclesService.GetAllAvailableAndCurrent(SelectedEmployee.Vehicle),
                HasVehicle = SelectedEmployee.Vehicle != null
            });
            await Task.Run(() =>
            {
                if (result != true)
                {
                    employeesesEmployeesService.Reload(SelectedEmployee);
                }
                else
                {
                    employeesesEmployeesService.Save();
                }
            });
        }

    }
}