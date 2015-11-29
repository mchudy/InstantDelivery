using Caliburn.Micro;
using InstantDelivery.Model;
using InstantDelivery.Model.Vehicles;
using InstantDelivery.ViewModel.Proxies;
using System;
using System.Collections.Generic;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    ///  Model widoku dodawania pojazdu.
    /// </summary>
    public class VehiclesAddViewModel : Screen
    {
        private readonly VehiclesServiceProxy vehiclesService;
        private bool addNewVehicleModel;
        private VehicleModelDto selectedVehicleModel;

        /// <summary>
        /// Konstruktor modelu widoku
        /// </summary>
        /// <param name="service"></param>
        public VehiclesAddViewModel(VehiclesServiceProxy service)
        {
            vehiclesService = service;
            NewVehicle = new AddVehicleDto();
            LoadModels();
        }

        private async void LoadModels()
        {
            VehicleModels = await vehiclesService.GetAllModels();
        }

        /// <summary>
        /// Kolekcja skojarzona z tabelą danych w widoku.
        /// </summary>
        public IList<VehicleModelDto> VehicleModels { get; set; }

        /// <summary>
        /// Aktualnie tworzony pojazd.
        /// </summary>
        public AddVehicleDto NewVehicle { get; set; }

        /// <summary>
        /// Aktualnie wybrany pojazd z tabeli danych.
        /// </summary>
        public VehicleModelDto SelectedVehicleModel
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
        public AddVehicleModelDto NewVehicleModel { get; set; } = new AddVehicleModelDto();

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
            if (AddNewVehicleModel)
            {
                var modelId = await vehiclesService.AddVehicleModel(NewVehicleModel);
                NewVehicle.VehicleModelId = modelId;
                await vehiclesService.AddVehicle(NewVehicle);
            }
            else
            {
                NewVehicle.VehicleModelId = SelectedVehicleModel.Id;
                await vehiclesService.AddVehicle(NewVehicle);
            }
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