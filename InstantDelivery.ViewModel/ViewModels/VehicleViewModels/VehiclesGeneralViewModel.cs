using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Core.Repositories;
using InstantDelivery.ViewModel.ViewModels;

namespace InstantDelivery.ViewModel
{
    public class VehiclesGeneralViewModel : PagingViewModel
    {
        private readonly VehiclesRepository repository;
        private readonly IWindowManager windowManager;
        private Vehicle selectedVehicle;
        private BindableCollection<Vehicle> vehicles;

        public VehiclesGeneralViewModel(VehiclesRepository repository, IWindowManager windowManager)
        {
            this.repository = repository;
            this.windowManager = windowManager;
            Vehicles = new BindableCollection<Vehicle>(repository.Page(CurrentPage, PageSize));
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

        public BindableCollection<Vehicle> Vehicles
        {
            get { return vehicles; }
            set
            {
                vehicles = value;
                NotifyOfPropertyChange();
            }
        }

        public override bool IsEnabledNextPage => CurrentPage * PageSize < repository.Total;

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
                repository.Reload(SelectedVehicle);
            }
            else
            {
                repository.Save();
            }
        }

        public void DeleteVehicle()
        {
            if (SelectedVehicle == null)
            {
                return;
            }
            var result = windowManager.ShowDialog(new EmployeeDeleteViewModel());
            if (result == true)
            {
                repository.Remove(SelectedVehicle);
                LoadPage();
            }
        }

        protected override void LoadPage()
        {
            Vehicles = new BindableCollection<Vehicle>(repository.Page(CurrentPage, PageSize));
        }
    }
}