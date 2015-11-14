using System.Linq;
using InstantDelivery.Domain.Entities;

namespace InstantDelivery.Services
{
    /// <summary>
    /// Interfejs warstwy serwisu paczek
    /// </summary>
    public interface IPackageService
    {
        /// <summary>
        /// Przypisuje paczkę do pracownika
        /// </summary>
        /// <param name="package"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        bool AssignPackage(Package package, Employee employee);

        /// <summary>
        /// Rejestruje paczkę w bazie danych
        /// </summary>
        /// <param name="package"></param>
        void RegisterPackage(Package package);

        /// <summary>
        /// Zwraca wszystkie paczki w bazie danych
        /// </summary>
        /// <returns></returns>
        IQueryable<Package> GetAll();

        /// <summary>
        /// Wczytuje obiekt danej paczki z bazy danych, ignorując wprowadzone zmiany
        /// </summary>
        /// <param name="selectedPackage"></param>
        void Reload(Package selectedPackage);

        /// <summary>
        /// Zapisuje aktualny stan kontekstu
        /// </summary>
        void Save();

        /// <summary>
        /// Usuwa paczkę z bazy danych
        /// </summary>
        /// <param name="selectedPackage"></param>
        void RemovePackage(Package selectedPackage);

        /// <summary>
        /// Oblicza koszt paczki na podstawie jej szczegółów
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        decimal CalculatePackageCost(Package package);

        /// <summary>
        /// Zwraca pracownika do którego przypisana jest dana paczka
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        Employee GetAssignedEmployee(Package package);

        /// <summary>
        /// Zwraca wszytskich pracowników, dla których przypisanie danej paczki
        /// nie przekroczy maksymalnej ładowności samochodu
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        IQueryable<Employee> GetAvailableEmployees(Package package);

        /// <summary>
        /// Oznacza paczkę jako dostarczoną i usuwa ją ze zbioru paczek 
        /// dostarczającego pracownika
        /// </summary>
        /// <param name="package"></param>
        void MarkAsDelivered(Package package);
    }
}