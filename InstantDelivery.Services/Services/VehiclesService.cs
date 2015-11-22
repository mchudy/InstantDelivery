using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Services.Paging;
using System.Linq;

namespace InstantDelivery.Services
{
    /// <summary>
    /// Warstwa serwisu pojazdów
    /// </summary>
    public class VehiclesService : IVehiclesService
    {
        private InstantDeliveryContext context;

        /// <summary>
        /// Konstruktor warstwy serwisu
        /// </summary>
        /// <param name="context"></param>
        public VehiclesService(InstantDeliveryContext context)
        {
            this.context = context;
        }

        public PagedResult<Vehicle> GetPage(PageQuery<Vehicle> query)
        {
            return PagingHelper.GetPagedResult(context.Vehicles.AsQueryable(), query);
        }

        /// <summary>
        /// Zwraca wszystkie modele pojazdów
        /// </summary>
        /// <returns></returns>
        public IQueryable<VehicleModel> GetAllModels()
        {
            return context.VehicleModels;
        }

        /// <summary>
        /// Aktualizuje dane pojqzdu
        /// </summary>
        /// <param name="vehicle"></param>
        public void Reload(Vehicle vehicle)
        {
            context.Entry(vehicle).Reload();
        }

        /// <summary>
        /// Usuwa pojazd do bazy danych
        /// </summary>
        /// <param name="vehicle"></param>
        public void Remove(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
            context.SaveChanges();
        }

        /// <summary>
        /// Dodaje pojazd do bazy danych
        /// </summary>
        /// <param name="vehicle"></param>
        public void AddVehicle(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
            context.SaveChanges();
        }

        /// <summary>
        /// Zapisuje aktualny stan
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Zwraca kolekcję wszystkich wolnych pojzdów i pojazdu wyspecyfikowanego
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public PagedResult<Vehicle> GetAllAvailableAndCurrent(Vehicle vehicle, PageQuery<Vehicle> query)
        {
            var vehicleId = vehicle?.Id;
            query.Filters.Add(e => e.Id == vehicleId || context.Employees.Count(em => em.Vehicle.Id == e.Id) == 0);
            return PagingHelper.GetPagedResult(context.Vehicles, query);
        }
    }
}