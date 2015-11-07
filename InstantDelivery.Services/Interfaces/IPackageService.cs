using System.Collections.Generic;
using InstantDelivery.Core.Entities;

namespace InstantDelivery.Services
{
    public interface IPackageService
    {
        bool AssignPackage(Package package, Employee employee);
        void RegisterPackage(Package package);
        IEnumerable<Package> GetAll();
        void Reload(Package selectedPackage);
        void Save();
        void RemovePackage(Package selectedPackage);
        void CalculatePackageCost(Package package);
    }
}