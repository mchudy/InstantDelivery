using InstantDelivery.Core.Entities;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Caliburn.Micro;

namespace InstantDelivery.Services
{
    public interface IVehiclesService
    {
        IQueryable<Vehicle> GetAll();
        void Reload(Vehicle vehicle);
        void Remove(Vehicle vehicle);
        void AddVehicle(Vehicle vehicle);
        void Save();
        IObservableCollection<Vehicle> GetAllAvailableAndCurrent(Vehicle vehicle);
    }
}