using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using System.Linq;

namespace InstantDelivery.ViewModel
{
    public class VehiclesGeneralViewModel : Screen
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

        public int CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                NotifyOfPropertyChange();
            }
        }

        public Vehicle SelectedVehicle
        {
            get { return selectedVehicle; }
            set
            {
                selectedVehicle = value;
                NotifyOfPropertyChange();
            }
        }

        public IQueryable<Vehicle> Vehicles
        {
            get { return vehicles; }
            set
            {
                vehicles = value;
                NotifyOfPropertyChange();
            }
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