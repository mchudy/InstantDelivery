using InstantDelivery.Core;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Linq;

namespace InstantDelivery.Services
{
    /// <summary>
    /// Warstwa serwisu statystyk
    /// </summary>
    public class StatisticsService : IStatisticsService
    {
        private readonly InstantDeliveryContext context;

        /// <summary>
        /// Konstruktor wasrty serwisu
        /// </summary>
        /// <param name="context"></param>
        public StatisticsService(InstantDeliveryContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Zwraca sumę wartości cen wszystkich paczek
        /// </summary>
        /// <returns></returns>
        public int ValueOfAllPackages()
        {
            return (int)context.Employees.Where(e => e.Packages.Any()).Sum(e => e.Packages.Sum(p => p.Cost));
        }

        /// <summary>
        /// Zwraca sumę wartości pensji wszystkich pracowników
        /// </summary>
        /// <returns></returns>
        public int EmployeesSalaries()
        {
            return (int)context.Employees.Sum(e => e.Salary);
        }

        /// <summary>
        /// Zwraca wartość podatków
        /// </summary>
        /// <param name="valueOfPackages"></param>
        /// <param name="employeesSalaries"></param>
        /// <returns></returns>
        public int Taxes(int valueOfPackages, int employeesSalaries)
        {
            return (int)((valueOfPackages * 0.25) + (employeesSalaries * 0.40));
        }

        /// <summary>
        /// Zwraca liczbę aktualnych pracowników
        /// </summary>
        /// <returns></returns>
        public int NumberOfEmployees()
        {
            return context.Employees.Count();
        }

        /// <summary>
        /// Zwraca liczbę aktualnych pojazdów
        /// </summary>
        /// <returns></returns>
        public int NumberOfVehicles()
        {
            return context.Vehicles.Count();
        }

        /// <summary>
        /// Zwraca liczbę dostarczanych paczek
        /// </summary>
        /// <returns></returns>
        public int NumberOfPackagesWithEmployee()
        {
            return context.Packages.Count(p => context.Employees.Count(e => e.Packages.Any(x => x.Id == p.Id)) == 1);
        }

        /// <summary>
        /// Zwraca liczbbę niedostarczanych paczek
        /// </summary>
        /// <returns></returns>
        public int NumberOfPackagesWithoutEmployee()
        {
            return context.Packages.Count(p => context.Employees.Count(e => e.Packages.Any(x => x.Id == p.Id)) == 0);
        }

        /// <summary>
        /// Zwraca liczbę wszystkich paczek
        /// </summary>
        /// <returns></returns>
        public int NumberOfAllPackages()
        {
            return context.Packages.Count();
        }

        /// <summary>
        /// Zwraca liczbę używanych pojazdów przez pracowników
        /// </summary>
        /// <returns></returns>
        public int NumberOfUsedVehicles()
        {
            return context.Vehicles.Count(p => context.Employees.Count(e => e.Vehicle.Id == p.Id) == 1);
        }

        /// <summary>
        /// Zwraca liczbę nieuzywanych pojazdów przez pracowników
        /// </summary>
        /// <returns></returns>
        public int NumberOfUnusedVehicles()
        {
            return context.Vehicles.Count(p => context.Employees.Count(e => e.Vehicle.Id == p.Id) == 0);
        }

        /// <summary>
        /// Generuje wykres statystyk ogólnych
        /// </summary>
        /// <param name="Values"></param>
        public void GenerateStatisticsEmployeesVehiclesChart(ObservableCollection<Population> Values)
        {
            var numberOfEmployees = NumberOfEmployees();
            var numberOfVehicles = NumberOfVehicles();
            var numberOfAllPackages = NumberOfAllPackages();
            var numberOfPackagesWithEmployee = NumberOfPackagesWithEmployee();
            var numberOfPackagesWithoutEmployee = NumberOfPackagesWithoutEmployee();
            var numberOfUsedVehicles = NumberOfUsedVehicles();
            var numberOfUnusedVehicles = NumberOfUnusedVehicles();
            Values.Add(new Population() { Name = "Liczba pracowników", Count = numberOfEmployees });

            Values.Add(new Population() { Name = "Liczba pojazdów", Count = numberOfVehicles });
            Values.Add(new Population() { Name = "Używane pojazdy", Count = numberOfUsedVehicles });
            Values.Add(new Population() { Name = "Nieużywane pojazdy", Count = numberOfUnusedVehicles });

            Values.Add(new Population() { Name = "Wszystkie paczki", Count = numberOfAllPackages });
            Values.Add(new Population() { Name = "Dostarczane paczki", Count = numberOfPackagesWithEmployee });
            Values.Add(new Population() { Name = "Wolne paczki", Count = numberOfPackagesWithoutEmployee });
        }
    }

    /// <summary>
    /// Pomocnicza klasa do konstrukcji wykresu
    /// </summary>
    [ImplementPropertyChanged]
    public class Population
    {
        public string Name { get; set; } = string.Empty;

        public int Count { get; set; } = 0;
    }
}