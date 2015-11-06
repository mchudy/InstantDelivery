using System;
using System.Collections.Generic;
using System.Linq;
using InstantDelivery.Core;
using InstantDelivery.Core.Entities;

namespace InstantDelivery.Services
{
    public class VehiclesRepository : IDisposable
    {
        //TODO DI
        private InstantDeliveryContext context = new InstantDeliveryContext();
        public int Total => context.Employees.Count();

        public IList<Vehicle> GetAll()
        {
            return context.Vehicles.ToList();
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

        //TODO to powinno być chyba jakieś extension method, zeby mozna bylo podpiac do kazdego zapytania
        public IList<Vehicle> Page(int pageNumber, int pageSize)
        {
            return context.Vehicles.OrderBy(e => e.Id)
                                    .Skip(pageSize * (pageNumber - 1))
                                    .Take(pageSize).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

    }
}