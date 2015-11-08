using System.ComponentModel;
using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using System.Linq;

namespace InstantDelivery.ViewModel
{
    public class VehiclesGeneralViewModel : VehiclesViewModelBase
    {
        private readonly IVehiclesService vehiclesService;
        private readonly IWindowManager windowManager;
        private Vehicle selectedVehicle;
        private IQueryable<Vehicle> vehicles;
        private int currentPage = 1;
        private VehicleEditViewModel vehiclesEditViewModel;
        private ConfirmDeleteViewModel confirmDeleteViewModel;

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

        public bool IsSelectedAnyRow
        {
            get { return SelectedVehicle != null; }
        }

        public void EditVehicle()
        {
            if (SelectedVehicle == null)
            {
                return;
            }
            vehiclesEditViewModel.Vehicle = SelectedVehicle;

            var result = windowManager.ShowDialog(vehiclesEditViewModel);

            if (result != true)
            {
                vehiclesService.Reload(SelectedVehicle);
            }
            else
            {
                vehiclesService.Save();
            }
        }

        public void DeleteVehicle()
        {
            if (SelectedVehicle == null)
            {
                return;
            }
            var result = windowManager.ShowDialog(confirmDeleteViewModel);
            if (result == true)
            {
                vehiclesService.Remove(SelectedVehicle);
                Vehicles = null;
                Vehicles = vehiclesService.GetAll();
            }
        }
    }
}