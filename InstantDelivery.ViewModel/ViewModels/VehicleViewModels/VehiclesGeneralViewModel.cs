using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using System.Linq;

namespace InstantDelivery.ViewModel
{
    public class VehiclesGeneralViewModel : Screen
    {
        private readonly VehiclesService vehiclesService;
        private readonly IWindowManager windowManager;
        private Vehicle selectedVehicle;
        private IQueryable<Vehicle> vehicles;
        private int currentPage = 1;

        public VehiclesGeneralViewModel(VehiclesService vehiclesService, IWindowManager windowManager)
        {
            this.vehiclesService = vehiclesService;
            this.windowManager = windowManager;
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
            var result = windowManager.ShowDialog(new VehicleEditViewModel
            {
                Vehicle = SelectedVehicle
            });
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
            var result = windowManager.ShowDialog(new ConfirmDeleteViewModel());
            if (result == true)
            {
                vehiclesService.Remove(SelectedVehicle);
                CurrentPage = CurrentPage;
            }
        }
    }
}