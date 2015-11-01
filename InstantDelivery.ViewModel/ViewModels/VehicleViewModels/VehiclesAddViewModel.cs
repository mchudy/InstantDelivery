using System;
using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Core.Repositories;

namespace InstantDelivery.ViewModel
{
    public class VehiclesAddViewModel : Screen
    {
        private readonly VehiclesRepository repository;
        private readonly IWindowManager windowManager;

        public VehiclesAddViewModel(VehiclesRepository repository, IWindowManager windowManager)
        {
            this.repository = repository;
            this.windowManager = windowManager;
            NewVehicle = new Vehicle();
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