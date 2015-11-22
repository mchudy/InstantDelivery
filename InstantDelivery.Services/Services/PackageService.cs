using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Services.Paging;
using InstantDelivery.Services.Pricing;
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

        public PagedResult<Package> GetPage(PageQuery<Package> query)
        {
            return PagingHelper.GetPagedResult(context.Packages.AsQueryable(), query);
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
        /// Zwraca pracownika do którego przypisana jest dana paczka
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public Employee GetAssignedEmployee(Package package)
        {
            if (package == null) return null;
            return context.Employees.FirstOrDefault(e => e.Packages.Count(p => p.Id == package.Id) > 0);
        }

        /// <summary>
        /// Zwraca wszytskich pracowników, dla których przypisanie danej paczki
        /// nie przekroczy maksymalnej ładowności samochodu
        /// </summary>
        /// <param name="package"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public PagedResult<Employee> GetAvailableEmployees(Package package, PageQuery<Employee> query)
        {
            query.Filters.Add(
                e => (double)(e.Packages.Where(p => p.Id != package.Id).Sum(p => p.Weight) + package.Weight) <
                            e.Vehicle.VehicleModel.Payload);
            return PagingHelper.GetPagedResult(context.Employees, query);
        }

        /// <summary>
        /// Oznacza paczkę jako dostarczoną i usuwa ją ze zbioru paczek 
        /// dostarczającego pracownika
        /// </summary>
        /// <param name="package"></param>
        public void MarkAsDelivered(Package package)
        {
            package.Status = PackageStatus.Delivered;
            var owner = context.Employees.FirstOrDefault(e => e.Packages.Any(p => p.Id == package.Id));
            owner?.Packages.Remove(package);
            context.SaveChanges();
        }

        /// <summary>
        /// Wczytuje obiekt danej paczki z bazy danych, ignorując wprowadzone zmiany
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