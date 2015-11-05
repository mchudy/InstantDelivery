using InstantDelivery.Core.Entities;

namespace InstantDelivery.Core
{
    public class PackageService
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
    }
}