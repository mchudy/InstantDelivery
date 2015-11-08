using InstantDelivery.Core.Entities;
using System.Linq;

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
        /// <summary
        /// Rejestruje paczkę w bazie danych.
        /// </summary>
        /// <param name="package"></param>
        void RegisterPackage(Package package);
        /// <summary>
        /// Zwraca wszystkie paczki w bazie danych
        /// </summary>
        /// <returns></returns>
        IQueryable<Package> GetAll();
        /// <summary>
        /// Aktualizuje dane paczki
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
    }
}