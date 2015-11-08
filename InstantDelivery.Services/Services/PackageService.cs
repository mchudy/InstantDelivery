using InstantDelivery.Core;
using InstantDelivery.Core.Entities;
using System.Linq;

namespace InstantDelivery.Services
{
    /// <summary>
    /// Warstwa serwisu paczek
    /// </summary>
    public class PackageService : IPackageService
    {
        private InstantDeliveryContext context;
        private IPricingStrategy pricingStrategy;
        /// <summary>
        /// Konstruktor warstwy serwisu
        /// </summary>
        /// <param name="context"></param>
        /// <param name="pricingStrategy"></param>
        public PackageService(InstantDeliveryContext context, IPricingStrategy pricingStrategy)
        {
            this.context = context;
            this.pricingStrategy = pricingStrategy;
        }
        /// <summary>
        /// Rejestruje daną paczkę w bazie danych
        /// </summary>
        /// <param name="package"></param>
        public void RegisterPackage(Package package)
        {
            package.Status = PackageStatus.New;
            package.Cost = pricingStrategy.GetCost(package);
            context.Packages.Add(package);
            context.SaveChanges();
        }
        /// <summary>
        /// Przypisuje paczkę do pracownika i zmienia jej status
        /// </summary>
        /// <param name="package"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
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
            //using (var transaction = context.Database.BeginTransaction())
            //{
            package.Status = PackageStatus.Delivered;
            var owner = context.Employees.FirstOrDefault(e => e.Packages.Any(p => p.Id == package.Id));
            owner?.Packages.Remove(package);
            context.SaveChanges();
            //  transaction.Commit();
            //}
        }

        public IQueryable<Package> GetAll()
        {
            return context.Packages;
        }
        /// <summary>
        /// Aktualizuje dane danej paczki
        /// </summary>
        /// <param name="selectedPackage"></param>
        public void Reload(Package selectedPackage)
        {
            context.Entry(selectedPackage).Reload();
        }
        /// <summary>
        /// Zapisuje aktualne zmiany
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }
        /// <summary>
        /// Usuwa paczkę z bazy danych
        /// </summary>
        /// <param name="selectedPackage"></param>
        public void RemovePackage(Package selectedPackage)
        {
            context.Packages.Remove(selectedPackage);
            context.SaveChanges();
        }
        /// <summary>
        /// Oblicza koszt paczki na podstawie jej szczegółów.
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public decimal CalculatePackageCost(Package package)
        {
            return pricingStrategy.GetCost(package);
        }
    }
}