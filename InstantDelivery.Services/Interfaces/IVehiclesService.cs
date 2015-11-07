using InstantDelivery.Core.Entities;
using System.Linq;

namespace InstantDelivery.Services
{
    public interface IVehiclesService
    {
        IQueryable<Vehicle> GetAll();
        IQueryable<VehicleModel> GetAllModels();
        void Reload(Vehicle vehicle);
        void Remove(Vehicle vehicle);
        void AddVehicle(Vehicle vehicle);
        void Save();
        IQueryable<Vehicle> GetAllAvailableAndCurrent(Vehicle vehicle);
    }
}