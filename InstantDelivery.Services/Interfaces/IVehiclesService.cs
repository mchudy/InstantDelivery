using InstantDelivery.Domain.Entities;
using InstantDelivery.Services.Paging;
using System.Linq;

namespace InstantDelivery.Services
{
    /// <summary>
    /// Interfejs warstwy serwisu pojazdów
    /// </summary>
    public interface IVehiclesService
    {
        /// <summary>
        /// Zwraca wszystkie modele pojazdów z bazy danych
        /// </summary>
        /// <returns></returns>
        IQueryable<VehicleModel> GetAllModels();

        /// <summary>
        /// Zwraca stronę z pojazdami
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        PagedResult<Vehicle> GetPage(PageQuery<Vehicle> query);

        /// <summary>
        /// Aktualizuje dane pojazdu
        /// </summary>
        /// <param name="vehicle"></param>
        void Reload(Vehicle vehicle);

        /// <summary>
        /// Usuwa pojazd z bazy danych
        /// </summary>
        /// <param name="vehicle"></param>
        void Remove(Vehicle vehicle);

        /// <summary>
        /// Dodaje pojazd do bazy danych
        /// </summary>
        /// <param name="vehicle"></param>
        void AddVehicle(Vehicle vehicle);

        /// <summary>
        /// Zapisuje aktualny stan kontekstu
        /// </summary>
        void Save();

        /// <summary>
        /// Zwraca wszystkie wolne pojazdy i wyspecyfikowany
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        IQueryable<Vehicle> GetAllAvailableAndCurrent(Vehicle vehicle);
    }
}