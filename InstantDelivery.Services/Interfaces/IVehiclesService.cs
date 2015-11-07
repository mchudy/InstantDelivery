using System.Collections.Generic;
using InstantDelivery.Core.Entities;
using System.Linq;
using Caliburn.Micro;

namespace InstantDelivery.Services
{
    public interface IVehiclesService
    {
        IQueryable<Vehicle> GetAll();
        void Reload(Vehicle vehicle);
        void Remove(Vehicle vehicle);
        void Save();
        IObservableCollection<Vehicle> GetAllAvailableAndCurrent(Vehicle vehicle);
    }
}