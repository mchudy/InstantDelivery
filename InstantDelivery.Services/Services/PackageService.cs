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

        //TODO sprawdzanie czy paczka mieści się w samochodzie / transakcja(?)
        public bool AssignPackage(Package package, Employee employee)
        {
            package.Status = PackageStatus.InDelivery;
            employee.Packages.Add(package);
            context.SaveChanges();
            return true;
        }

        public Employee GetAssignedEmployee(Package package)
        {
            if (package == null) return null;
            return context.Employees.FirstOrDefault(e => e.Packages.Count(p => p.Id == package.Id) > 0);
        }

        public IQueryable<Employee> GetAvailableEmployees(Package package)
        {
            return context.Employees
                .Where(e => (double)(e.Packages.Where(p => p.Id != package.Id).Sum(p => p.Weight) + package.Weight) <
                            e.Vehicle.VehicleModel.Payload);
        }

        public void MarkAsDelivered(Package package)
        {
            package.Status = PackageStatus.Delivered;
            var owner = context.Employees.FirstOrDefault(e => e.Packages.Contains(package));
            owner?.Packages.Remove(package);
            context.SaveChanges();
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