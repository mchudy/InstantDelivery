
using System.Collections.Generic;
using InstantDelivery.Core;
using InstantDelivery.Core.Entities;

namespace InstantDelivery.Services
{
    public class PackageService : IPackageService
    {
        private InstantDeliveryContext context;
        private IPricingStrategy pricingStrategy;

        public PackageService(InstantDeliveryContext context, IPricingStrategy pricingStrategy)
        {
            this.context = context;
            this.pricingStrategy = pricingStrategy;
        }

        public void RegisterPackage(Package package)
        {
            package.Status = PackageStatus.New;
            package.Cost = pricingStrategy.GetCost(package);
            context.Packages.Add(package);
            context.SaveChanges();
        }

        //TODO sprawdzanie czy paczka mieści się w samochodzie / transakcja(?)
        public bool AssignPackage(Package package, Employee employee)
        {
            package.Status = PackageStatus.InDelivery;
            employee.Packages.Add(package);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<Package> GetAll()
        {
            return context.Packages;
        }

        public void Reload(Package selectedPackage)
        {
            context.Entry(selectedPackage).Reload();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void RemovePackage(Package selectedPackage)
        {
            context.Packages.Remove(selectedPackage);
            context.SaveChanges();
        }

        public void CalculatePackageCost(Package package)
        {
            if (package == null)
                return;
            package.Cost = pricingStrategy.GetCost(package);
        }

        public void AddPackage(Package package)
        {
            context.Packages.Add(package);
            context.SaveChanges();
        }
    }
}