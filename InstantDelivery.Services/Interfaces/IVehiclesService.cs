using InstantDelivery.Core.Entities;
using System.Linq;

namespace InstantDelivery.Services
{
    /// <summary>
    /// Interfejs warstwy serwisu pojazdów
    /// </summary>
    public interface IVehiclesService
    {
        /// <summary>
        /// Zwraca wszystkie pojazdy z bazy danych
        /// </summary>
        /// <returns></returns>
        IQueryable<Vehicle> GetAll();

        /// <summary>
        /// Zwraca wszystkie modele pojazdów z bazy danych
        /// </summary>
        /// <returns></returns>
        IQueryable<VehicleModel> GetAllModels();

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