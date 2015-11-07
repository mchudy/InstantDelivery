using System.Collections.ObjectModel;

namespace InstantDelivery.Services
{
    public interface IStatisticsService
    {
        int EmployeesSalaries();
        void GenerateStatisticsEmployeesVehiclesChart(ObservableCollection<Population> Values);
        int NumberOfAllPackages();
        int NumberOfEmployees();
        int NumberOfPackagesWithEmployee();
        int NumberOfPackagesWithoutEmployee();
        int NumberOfUnusedVehicles();
        int NumberOfUsedVehicles();
        int NumberOfVehicles();
        int Taxes(int valueOfPackages, int employeesSalaries);
        int ValueOfAllPackages();
    }
}