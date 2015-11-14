using Caliburn.Micro;
using InstantDelivery.Services;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using InstantDelivery.Domain.Entities;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Ogólny model widoku pojazdów.
    /// </summary>
    public class VehiclesGeneralViewModel : VehiclesViewModelBase
    {
        private readonly IVehiclesService vehiclesService;
        private readonly IWindowManager windowManager;
        private Vehicle selectedVehicle;
        private VehicleEditViewModel vehiclesEditViewModel;
        private ConfirmDeleteViewModel confirmDeleteViewModel;

        /// <summary>
        /// Konstruktor widoku.
        /// </summary>
        /// <param name="vehiclesService"></param>
        /// <param name="windowManager"></param>
        /// <param name="vehiclesEditViewModel"></param>
        /// <param name="confirmDeleteViewModel"></param>
        public VehiclesGeneralViewModel(IVehiclesService vehiclesService, IWindowManager windowManager,
           VehicleEditViewModel vehiclesEditViewModel, ConfirmDeleteViewModel confirmDeleteViewModel)
        {
            this.vehiclesService = vehiclesService;
            this.windowManager = windowManager;
            this.vehiclesEditViewModel = vehiclesEditViewModel;
            this.confirmDeleteViewModel = confirmDeleteViewModel;
            Vehicles = vehiclesService.GetAll();
        }

        protected override IQueryable<Vehicle> GetVehicles()
        {
            return vehiclesService.GetAll();
        }

        /// <summary>
        /// Aktualnie zaznaczony wiersz tabeli danych.
        /// </summary>
        public Vehicle SelectedVehicle
        {
            get { return selectedVehicle; }
            set
            {
                selectedVehicle = value;
                NotifyOfPropertyChange();
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsSelectedAnyRow)));
            }
        }

        /// <summary>
        /// Flaga informująca o tym czy jest zaznaczony aktualnie jakiś wiersz w tabeli danych.
        /// </summary>
        public bool IsSelectedAnyRow => SelectedVehicle != null;

        /// <summary>
        /// Delegat zdarzenia przejścia do widoku edycji pojazdu.
        /// </summary>
        public async void EditVehicle()
        {
            if (SelectedVehicle == null)
            {
                return;
            }
            vehiclesEditViewModel.Vehicle = SelectedVehicle;

            var result = windowManager.ShowDialog(vehiclesEditViewModel);
            await Task.Run(() =>
            {
                if (result != true)
                {
                    vehiclesService.Reload(SelectedVehicle);
                }
                else
                {
                    vehiclesService.Save();
                }
            });
        }

        /// <summary>
        /// Delegat zdarzenia usuwania pojazdu.
        /// </summary>
        public async void DeleteVehicle()
        {
            if (SelectedVehicle == null)
            {
                return;
            }
            var result = windowManager.ShowDialog(confirmDeleteViewModel);
            await Task.Run(() =>
            {
                if (result == true)
                {
                    vehiclesService.Remove(SelectedVehicle);
                    Vehicles = null;
                    Vehicles = vehiclesService.GetAll();
                }
            });
        }
    }
}