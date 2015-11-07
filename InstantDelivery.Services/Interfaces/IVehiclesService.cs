using System.Linq;
using InstantDelivery.Core.Entities;

namespace InstantDelivery.Services
{
    public interface IVehiclesService
    {
        int Total { get; }

        IQueryable<Vehicle> GetAll();
        void Reload(Vehicle vehicle);
        void Remove(Vehicle vehicle);
        void Save();
    }
}