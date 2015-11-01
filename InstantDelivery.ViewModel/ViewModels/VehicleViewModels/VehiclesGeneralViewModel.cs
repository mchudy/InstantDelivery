using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Core.Repositories;

namespace InstantDelivery.ViewModel
{
    public class VehiclesGeneralViewModel : Screen
    {
        private readonly VehiclesRepository repository;
        private readonly IWindowManager windowManager;
        private Vehicle selectedVehicle;
        private int currentPage = 1;
        private int pageSize = 10;
        private BindableCollection<Vehicle> vehicles;

        public VehiclesGeneralViewModel(VehiclesRepository repository, IWindowManager windowManager)
        {
            this.repository = repository;
            this.windowManager = windowManager;
            Vehicles = new BindableCollection<Vehicle>(repository.Page(CurrentPage, pageSize));
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

        public int CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => IsEnabledPreviousPage);
                NotifyOfPropertyChange(() => IsEnabledNextPage);
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

        public void NextPage()
        {
            CurrentPage++;
            LoadPage();
        }

        public bool IsEnabledNextPage => currentPage * pageSize < repository.Total;

        public bool IsEnabledPreviousPage => currentPage != 1;

        public void PreviousPage()
        {
            if (CurrentPage == 1) return;
            CurrentPage--;
            LoadPage();
        }

        public void Sort()
        {
            CurrentPage = 1;
            LoadPage();
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

        private void LoadPage()
        {
            Vehicles = new BindableCollection<Vehicle>(repository.Page(CurrentPage, pageSize));
        }
    }
}