using Caliburn.Micro;
using InstantDelivery.Services;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel
{
    public class StatisticsEmployeesVehiclesViewModel : Screen
    {
        private IStatisticsService service;

        public StatisticsEmployeesVehiclesViewModel(IStatisticsService service)
        {
            this.service = service;
            GenerateChart();
        }

        public BindableCollection<Population> Values { get; } = new BindableCollection<Population>();

        private async void GenerateChart()
        {
            await Task.Run(() =>
            {
                var numberOfEmployees = service.NumberOfEmployees();
                var numberOfVehicles = service.NumberOfVehicles();
                var numberOfAllPackages = service.NumberOfAllPackages();
                var numberOfPackagesWithEmployee = service.NumberOfPackagesWithEmployee();
                var numberOfPackagesWithoutEmployee = service.NumberOfPackagesWithoutEmployee();
                var numberOfUsedVehicles = service.NumberOfUsedVehicles();
                var numberOfUnusedVehicles = service.NumberOfUnusedVehicles();
                Values.Add(new Population() { Name = "Liczba pracowników", Count = numberOfEmployees });

                Values.Add(new Population() { Name = "Liczba pojazdów", Count = numberOfVehicles });
                Values.Add(new Population() { Name = "Używane pojazdy", Count = numberOfUsedVehicles });
                Values.Add(new Population() { Name = "Nieużywane pojazdy", Count = numberOfUnusedVehicles });

                Values.Add(new Population() { Name = "Wszystkie paczki", Count = numberOfAllPackages });
                Values.Add(new Population() { Name = "Dostarczane paczki", Count = numberOfPackagesWithEmployee });
                Values.Add(new Population() { Name = "Niedostarczane paczki", Count = numberOfPackagesWithoutEmployee });
            });
        }
    }
}