using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel
{
    public class VehiclesGeneralViewModel : VehiclesViewModelBase
    {
        private readonly IVehiclesService vehiclesService;
        private readonly IWindowManager windowManager;
        private Vehicle selectedVehicle;
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

        public bool IsSelectedAnyRow => SelectedVehicle != null;

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