using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Domain.Extensions;
using InstantDelivery.Services.Paging;
using System;
using System.ComponentModel;
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

        /// <summary>
        /// Zwraca wszystkie pojazdy
        /// </summary>
        /// <returns></returns>
        public IQueryable<Vehicle> GetAll()
        {
            return context.Vehicles;
        }

        public PagedResult<Vehicle> GetPage(PageQuery<Vehicle> query)
        {
            var result = context.Vehicles.AsQueryable();
            if (string.IsNullOrEmpty(query.SortProperty))
            {
                result = result.OrderBy(e => e.Id);
            }
            else if (query.SortDirection == ListSortDirection.Descending)
            {
                result = result.OrderByDescendingProperty(query.SortProperty);
            }
            else
            {
                result = result.OrderByProperty(query.SortProperty);
            }
            foreach (var filter in query.Filters)
            {
                result = result.Where(filter);
            }
            var pageCount = (int)Math.Ceiling(result.Count() / (double)query.PageSize);
            return new PagedResult<Vehicle>
            {
                PageCount = pageCount,
                PageCollection = result.Page(query.PageIndex, query.PageSize)
            };
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
        /// <returns></returns>
        public IQueryable<Vehicle> GetAllAvailableAndCurrent(Vehicle vehicle)
        {
            var vehicleId = vehicle?.Id;
            return context.Vehicles
                .Where(e => (e.Id == vehicleId || context.Employees.Count(em => em.Vehicle.Id == e.Id) == 0));
        }
    }
}