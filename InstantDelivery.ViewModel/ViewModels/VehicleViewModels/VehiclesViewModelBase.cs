using System.ComponentModel;
using System.Linq;
using Caliburn.Micro;
using InstantDelivery.Core.Entities;

namespace InstantDelivery.ViewModel
{
    public abstract class VehiclesViewModelBase : Screen
    {
        private IQueryable<Vehicle> vehicles;

        private int currentPage = 1;
        private string brandFilter = string.Empty;
        private string modelFilter = string.Empty;
        private string registrationNumberFilter = string.Empty;
        private VehicleSortingProperty? sortingProperty;

        public IQueryable<Vehicle> Vehicles
        {
            get { return vehicles; }
            set
            {
                vehicles = value;
                NotifyOfPropertyChange();
            }
        }

        public string BrandFilter
        {
            get { return brandFilter; }
            set
            {
                brandFilter = value;
                UpdateVehicles();
            }
        }

        public string ModelFilter
        {
            get { return modelFilter; }
            set
            {
                modelFilter = value;
                UpdateVehicles();
            }
        }

        protected abstract IQueryable<Vehicle> GetVehicles();

        protected void UpdateVehicles()
        {
            var newVehicles = GetVehicles();
            if (SortingProperty != null)
            {
                newVehicles = SortVehicles(newVehicles);
            }
            newVehicles = FilterVehicles(newVehicles);
            Vehicles = newVehicles;
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

        public string RegistrationNumberFilter
        {
            get { return registrationNumberFilter; }
            set
            {
                registrationNumberFilter = value;
                UpdateVehicles();
            }
        }

        public VehicleSortingProperty? SortingProperty
        {
            get { return sortingProperty; }
            set
            {
                sortingProperty = value;
                UpdateVehicles();
            }
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            UpdateVehicles();
        }

        private IQueryable<Vehicle> SortVehicles(IQueryable<Vehicle> newVehicles)
        {
            if (SortingProperty == VehicleSortingProperty.ByMark)
            {
                newVehicles = newVehicles.OrderBy(e => e.VehicleModel.Brand);
                CurrentPage = 1;
            }
            else if (SortingProperty == VehicleSortingProperty.ByRegistrationNumber)
            {
                newVehicles = newVehicles.OrderBy(e => e.RegistrationNumber);
                CurrentPage = 1;
            }
            else if (SortingProperty == VehicleSortingProperty.ByModel)
            {
                newVehicles = newVehicles.OrderBy(e => e.VehicleModel.Model);
                CurrentPage = 1;
            }
            return newVehicles;
        }

        private IQueryable<Vehicle> FilterVehicles(IQueryable<Vehicle> newVehicles)
        {
            return newVehicles
                .Where(e => BrandFilter == "" || e.VehicleModel.Brand.StartsWith(BrandFilter))
                .Where(e => ModelFilter == "" || e.VehicleModel.Model.StartsWith(ModelFilter))
                .Where(e => RegistrationNumberFilter == "" || e.RegistrationNumber.StartsWith(RegistrationNumberFilter));
        }
    }

    public enum VehicleSortingProperty
    {
        [Description("Po marce")]
        ByMark,
        [Description("Po modelu")]
        ByModel,
        [Description("Po numerze rejestracyjnym")]
        ByRegistrationNumber
    }
}