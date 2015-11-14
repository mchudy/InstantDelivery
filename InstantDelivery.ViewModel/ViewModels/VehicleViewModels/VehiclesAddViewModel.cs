using Caliburn.Micro;
using InstantDelivery.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstantDelivery.Domain.Entities;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    ///  Model widoku dodawania pojazdu.
    /// </summary>
    public class VehiclesAddViewModel : Screen
    {
        private IVehiclesService vehiclesService;
        private bool addNewVehicleModel;
        private VehicleModel selectedVehicleModel;

        /// <summary>
        /// Konstruktor modelu widoku
        /// </summary>
        /// <param name="service"></param>
        public VehiclesAddViewModel(IVehiclesService service)
        {
            this.vehiclesService = service;
            NewVehicle = new Vehicle();
            VehicleModels = vehiclesService.GetAllModels().ToList();
        }

        /// <summary>
        /// Kolekcja skojarzona z tabelą danych w widoku.
        /// </summary>
        public IEnumerable<VehicleModel> VehicleModels { get; set; }

        /// <summary>
        /// Aktualnie tworzony pojazd.
        /// </summary>
        public Vehicle NewVehicle { get; set; }

        /// <summary>
        /// Aktualnie wybrany pojazd z tabeli danych.
        /// </summary>
        public VehicleModel SelectedVehicleModel
        {
            get { return selectedVehicleModel; }
            set
            {
                selectedVehicleModel = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Aktualnie tworzony model pojazdu.
        /// </summary>
        public VehicleModel NewVehicleModel { get; set; } = new VehicleModel();

        /// <summary>
        /// Flaga informująca o tym czy użytkownik tworzy nowy model pojazdu.
        /// </summary>
        public bool AddNewVehicleModel
        {
            get { return addNewVehicleModel; }
            set
            {
                addNewVehicleModel = value;
                if (value)
                {
                    SelectedVehicleModel = null;
                }
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Zapisuje zmiany dokonane w widoku.
        /// </summary>
        public async void Save()
        {
            var vehicleModel = AddNewVehicleModel ? NewVehicleModel : SelectedVehicleModel;
            NewVehicle.VehicleModel = vehicleModel;
            await Task.Run(() => vehiclesService.AddVehicle(NewVehicle));
            TryClose(true);
        }

        /// <summary>
        /// Anuluje zmiany dokonane w widoku.
        /// </summary>
        public void Cancel()
        {
            TryClose(false);
        }

        protected override void OnDeactivate(bool close)
        {
            if (close)
                NewVehicle = null;
        }

        public override void CanClose(Action<bool> callback)
        {
            callback(true);
        }
    }
}