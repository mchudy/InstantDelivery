using System.Collections.Generic;
using System.Linq;
using InstantDelivery.Core.Entities;
using System.Collections.Generic;

namespace InstantDelivery.Services
{
    public interface IPackageService
    {
        bool AssignPackage(Package package, Employee employee);
        void RegisterPackage(Package package);
        IQueryable<Package> GetAll();
        void Reload(Package selectedPackage);
        void Save();
        void RemovePackage(Package selectedPackage);
        decimal CalculatePackageCost(Package package);
    }
}