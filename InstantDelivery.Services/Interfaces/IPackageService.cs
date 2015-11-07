using InstantDelivery.Core.Entities;
using System.Collections.Generic;

namespace InstantDelivery.Services
{
    public interface IPackageService
    {
        bool AssignPackage(Package package, Employee employee);
        void AddPackage(Package package);
        IEnumerable<Package> GetAll();
        void Reload(Package selectedPackage);
        void Save();
        void RemovePackage(Package selectedPackage);
        decimal CalculatePackageCost(Package package);
    }
}