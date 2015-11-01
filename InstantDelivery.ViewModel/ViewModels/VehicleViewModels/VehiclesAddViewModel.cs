using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using System;

namespace InstantDelivery.ViewModel
{
    public class VehiclesAddViewModel : Screen
    {
        public VehiclesAddViewModel()
        {
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