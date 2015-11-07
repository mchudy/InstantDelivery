using InstantDelivery.Core;
using InstantDelivery.Core.Entities;
using System.Linq;

namespace InstantDelivery.Services
{
    public class VehiclesService : IVehiclesService
    {
        //TODO DI
        private InstantDeliveryContext context = new InstantDeliveryContext();
        public int Total => context.Employees.Count();

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