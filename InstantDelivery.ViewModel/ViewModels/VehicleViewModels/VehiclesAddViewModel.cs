using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InstantDelivery.ViewModel
{
    public class VehiclesAddViewModel : Screen
    {
        private IVehiclesService vehiclesService;

        public VehiclesAddViewModel(IVehiclesService vehiclesService)
        {
            this.vehiclesService = vehiclesService;
            NewVehicle = new Vehicle();
            VehicleModels = vehiclesService.GetAllModels().ToList();
        }

        public IEnumerable<VehicleModel> VehicleModels { get; set; }

        protected override void OnDeactivate(bool close)
        {
            if (close)
                NewVehicle = null;
        }

        public override void CanClose(Action<bool> callback)
        {
            callback(true);
        }

        private Vehicle newVehicle;
        public Vehicle NewVehicle
        {
            get { return newVehicle; }
            set
            {
                newVehicle = value;
                NotifyOfPropertyChange();
            }
        }

        public void Save()
        {
            TryClose(true);
        }

        public void Cancel()
        {
            TryClose(false);
        }
    }
}