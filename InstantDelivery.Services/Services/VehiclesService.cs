using InstantDelivery.Core;
using InstantDelivery.Core.Entities;
using System.Linq;

namespace InstantDelivery.Services
{
    public class VehiclesService : IVehiclesService
    {
        private InstantDeliveryContext context;

        public VehiclesService(InstantDeliveryContext context)
        {
            this.context = context;
        }

        public IQueryable<Vehicle> GetAll()
        {
            return context.Vehicles;
        }

        public IQueryable<VehicleModel> GetAllModels()
        {
            return context.VehicleModels;
        }

        public void Reload(Vehicle vehicle)
        {
            context.Entry(vehicle).Reload();
        }

        public void Remove(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void AddVehicle(Vehicle newVehicle)
        {
            context.Vehicles.Add(newVehicle);
            context.SaveChanges();
        }
    }
}