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
    }
}