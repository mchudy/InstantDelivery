using System.Collections.ObjectModel;

namespace InstantDelivery.Services
{
    /// <summary>
    /// Interfejs warstwy serwisu statystyk
    /// </summary>
    public interface IStatisticsService
    {
        /// <summary>
        /// Zwraca wartość pensji pracowników
        /// </summary>
        /// <returns></returns>
        int EmployeesSalaries();

        /// <summary>
        /// Generuje statystyki budżetu
        /// </summary>
        /// <param name="Values"></param>
        void GenerateStatisticsEmployeesVehiclesChart(ObservableCollection<Population> Values);

        /// <summary>
        /// Zwraca ilość paczek w bazie
        /// </summary>
        /// <returns></returns>
        int NumberOfAllPackages();

        /// <summary>
        /// Zwraca ilość pracowników w firmie
        /// </summary>
        /// <returns></returns>
        int NumberOfEmployees();

        /// <summary>
        /// Zwraca ilość niedostarczanych paczek
        /// </summary>
        /// <returns></returns>
        int NumberOfPackagesWithEmployee();

        /// <summary>
        /// Zwraca ilość dostarczanych paczek
        /// </summary>
        /// <returns></returns>
        int NumberOfPackagesWithoutEmployee();

        /// <summary>
        /// Zwraca ilość nieużywanych pojazdów
        /// </summary>
        /// <returns></returns>
        int NumberOfUnusedVehicles();

        /// <summary>
        /// Zwraca ilość nieużywanych pojazdów
        /// </summary>
        /// <returns></returns>
        int NumberOfUsedVehicles();

        /// <summary>
        /// Zwraca ilośc pojazdów w bazie
        /// </summary>
        /// <returns></returns>
        int NumberOfVehicles();

        /// <summary>
        /// Zwraca wartość podatków
        /// </summary>
        /// <param name="valueOfPackages"></param>
        /// <param name="employeesSalaries"></param>
        /// <returns></returns>
        int Taxes(int valueOfPackages, int employeesSalaries);

        /// <summary>
        /// Zwraca sumę kosztów wszystkich paczek
        /// </summary>
        /// <returns></returns>
        int ValueOfAllPackages();
    }
}