using InstantDelivery.Core.Entities;
using System.Linq;

namespace InstantDelivery.Services
{
    public interface IVehiclesService
    {
        IQueryable<Vehicle> GetAll();
        void Reload(Vehicle vehicle);
        void Remove(Vehicle vehicle);
        void Save();
    }
}