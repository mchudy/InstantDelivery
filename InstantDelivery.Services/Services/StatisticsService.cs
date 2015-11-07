using InstantDelivery.Core;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Linq;

namespace InstantDelivery.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly InstantDeliveryContext context;

        public StatisticsService(InstantDeliveryContext context)
        {
            this.context = context;
        }

        public int ValueOfAllPackages()
        {
            return (int)context.Employees.Where(e => e.Packages.Any()).Sum(e => e.Packages.Sum(p => p.Cost));
        }

        public int EmployeesSalaries()
        {
            return (int)context.Employees.Sum(e => e.Salary);
        }

        public int Taxes(int valueOfPackages, int employeesSalaries)
        {
            return (int)((valueOfPackages * 0.25) + (employeesSalaries * 0.40));
        }

        public int NumberOfEmployees()
        {
            return context.Employees.Count();
        }

        public int NumberOfVehicles()
        {
            return context.Vehicles.Count();
        }

        public int NumberOfPackagesWithEmployee()
        {
            return context.Packages.Count(p => context.Employees.Count(e => e.Packages.Any(x => x.Id == p.Id)) == 1);
        }

        public int NumberOfPackagesWithoutEmployee()
        {
            return context.Packages.Count(p => context.Employees.Count(e => e.Packages.Any(x => x.Id == p.Id)) == 0);
        }

        public int NumberOfAllPackages()
        {
            return context.Packages.Count();
        }

        public int NumberOfUsedVehicles()
        {
            return context.Vehicles.Count(p => context.Employees.Count(e => e.Vehicle.Id == p.Id) == 1);
        }

        public int NumberOfUnusedVehicles()
        {
            return context.Vehicles.Count(p => context.Employees.Count(e => e.Vehicle.Id == p.Id) == 0);
        }

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
    [ImplementPropertyChanged]
    public class Population
    {
        public string Name { get; set; } = string.Empty;

        public int Count { get; set; } = 0;
    }
}