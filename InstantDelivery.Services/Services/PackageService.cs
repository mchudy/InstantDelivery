using InstantDelivery.Core;
using InstantDelivery.Core.Entities;
using System.Linq;

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

        public bool AssignPackage(Package package, Employee employee)
        {
            package.Status = PackageStatus.InDelivery;
            employee.Packages.Add(package);
            context.SaveChanges();
            return true;
        }

        public IQueryable<Package> GetAll()
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

        public decimal CalculatePackageCost(Package package)
        {
            return pricingStrategy.GetCost(package);
        }
    }
}