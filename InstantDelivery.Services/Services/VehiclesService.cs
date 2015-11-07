using System.Collections.Generic;
using InstantDelivery.Core;
using InstantDelivery.Core.Entities;
using System.Linq;
using Caliburn.Micro;

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

        public IObservableCollection<Vehicle> GetAllAvailableAndCurrent(Vehicle vehicle)
        {
            return
                context.Vehicles.Where(e => (e.Id == vehicle.Id || context.Employees.Count(em => em.Vehicle.Id == e.Id) == 1)) as IObservableCollection<Vehicle>;
        }
    }
}