using Caliburn.Micro;
using InstantDelivery.Model;
using InstantDelivery.ViewModel.Proxies;
using System.ComponentModel;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Ogólny model widoku pojazdów.
    /// </summary>
    public class VehiclesGeneralViewModel : VehiclesViewModelBase
    {
        private readonly VehiclesServiceProxy vehiclesService;
        private readonly IWindowManager windowManager;
        private VehicleDto selectedVehicle;
        private VehicleEditViewModel vehiclesEditViewModel;
        private ConfirmDeleteViewModel confirmDeleteViewModel;

        /// <summary>
        /// Konstruktor widoku.
        /// </summary>
        /// <param name="vehiclesService"></param>
        /// <param name="windowManager"></param>
        /// <param name="vehiclesEditViewModel"></param>
        /// <param name="confirmDeleteViewModel"></param>
        public VehiclesGeneralViewModel(VehiclesServiceProxy vehiclesService, IWindowManager windowManager,
           VehicleEditViewModel vehiclesEditViewModel, ConfirmDeleteViewModel confirmDeleteViewModel)
            : base(vehiclesService)
        {
            this.vehiclesService = vehiclesService;
            this.windowManager = windowManager;
            this.vehiclesEditViewModel = vehiclesEditViewModel;
            this.confirmDeleteViewModel = confirmDeleteViewModel;
        }

        /// <summary>
        /// Aktualnie zaznaczony wiersz tabeli danych.
        /// </summary>
        public VehicleDto SelectedVehicle
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
            if (result != true)
            {
                var oldVehicle = await vehiclesService.GetById(selectedVehicle.Id);
                RefreshRow(oldVehicle);
            }
            else
            {
                await vehiclesService.UpdateVehicle(SelectedVehicle);
                UpdateData();
            }
        }

        private void RefreshRow(VehicleDto oldEmployee)
        {
            int index = Vehicles.IndexOf(SelectedVehicle);
            Vehicles.Remove(SelectedVehicle);
            Vehicles.Insert(index, oldEmployee);
            SelectedVehicle = oldEmployee;
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
            if (result == true)
            {
                await vehiclesService.DeleteVehicle(SelectedVehicle.Id);
                UpdateData();
            }
        }
    }
}