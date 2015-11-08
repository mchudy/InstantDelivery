using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Bazowy modeul widoku dla widoków pracowników.
    /// </summary>
    public abstract class VehiclesViewModelBase : Screen
    {
        private IQueryable<Vehicle> vehicles;

        private int currentPage = 1;
        private string brandFilter = string.Empty;
        private string modelFilter = string.Empty;
        private string registrationNumberFilter = string.Empty;
        private VehicleSortingProperty? sortingProperty;

        /// <summary>
        /// Kolekcja skojarzona z taelą danych.
        /// </summary>
        public IQueryable<Vehicle> Vehicles
        {
            get { return vehicles; }
            set
            {
                vehicles = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Filtr po marce wpisany przez użytkownika.
        /// </summary>
        public string BrandFilter
        {
            get { return brandFilter; }
            set
            {
                brandFilter = value;
                UpdateVehicles();
            }
        }

        /// <summary>
        /// Filtr po modelu wpisany przez użytkownika.
        /// </summary>
        public string ModelFilter
        {
            get { return modelFilter; }
            set
            {
                modelFilter = value;
                UpdateVehicles();
            }
        }

        /// <summary>
        /// Bieżąca strona
        /// </summary>
        public int CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Filtr numeru rejestracyjnego wprowadzony przez użytkownika.
        /// </summary>
        public string RegistrationNumberFilter
        {
            get { return registrationNumberFilter; }
            set
            {
                registrationNumberFilter = value;
                UpdateVehicles();
            }
        }

        /// <summary>
        /// Kryterium sortowania wybrane przez użytkownika.
        /// </summary>
        public VehicleSortingProperty? SortingProperty
        {
            get { return sortingProperty; }
            set
            {
                sortingProperty = value;
                UpdateVehicles();
            }
        }

        protected abstract IQueryable<Vehicle> GetVehicles();

        protected async void UpdateVehicles()
        {
            await Task.Run(() =>
            {
                var newVehicles = GetVehicles();
                if (SortingProperty != null)
                {
                    newVehicles = SortVehicles(newVehicles);
                }
                newVehicles = FilterVehicles(newVehicles);
                Vehicles = newVehicles;
            });
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
    /// <summary>
    /// Definicja kryterium sortowania.
    /// </summary>
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