using Caliburn.Micro;
using InstantDelivery.Services;
using System.Collections.ObjectModel;

namespace InstantDelivery.ViewModel
{
    public class StatisticsDeliveredPackagesViewModel : Screen
    {
        private IStatisticsService service;

        public StatisticsDeliveredPackagesViewModel(IStatisticsService service)
        {
            this.service = service;
            GenerateChart();
        }

        public ObservableCollection<Population> Budget { get; } = new ObservableCollection<Population>();

        private void GenerateChart()
        {
            var valueOfPackages = service.ValueOfAllPackages();
            var employeesSalaries = service.EmployeesSalaries();
            var taxes = service.Taxes(valueOfPackages, employeesSalaries);
            Budget.Add(new Population() { Name = "Wartość dostarczanych paczek", Count = valueOfPackages });
            Budget.Add(new Population() { Name = "Pensje pracowników", Count = employeesSalaries });
            Budget.Add(new Population() { Name = "Podatki", Count = taxes });
        }
    }
}