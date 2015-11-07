using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel
{
    public class VehiclesAddViewModel : Screen
    {
        private IVehiclesService vehiclesService;
        private bool addNewVehicleModel;
        private VehicleModel selectedVehicleModel;

        public VehiclesAddViewModel(IVehiclesService service)
        {
            this.vehiclesService = service;
            NewVehicle = new Vehicle();
            //TODO
            VehicleModels = vehiclesService.GetAllModels().ToList();
        }

        public IEnumerable<VehicleModel> VehicleModels { get; set; }

        public Vehicle NewVehicle { get; set; }

        public VehicleModel SelectedVehicleModel
        {
            get { return selectedVehicleModel; }
            set
            {
                selectedVehicleModel = value;
                NotifyOfPropertyChange();
            }
        }

        public VehicleModel NewVehicleModel { get; set; } = new VehicleModel();

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

        public async void Save()
        {
            var vehicleModel = AddNewVehicleModel ? NewVehicleModel : SelectedVehicleModel;
            NewVehicle.VehicleModel = vehicleModel;
            await Task.Run(() => vehiclesService.AddVehicle(NewVehicle));
            TryClose(true);
        }

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