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
        /// <summary>
        /// Zwraca wszystkie paczki z bazy danych
        /// </summary>
        /// <returns></returns>
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