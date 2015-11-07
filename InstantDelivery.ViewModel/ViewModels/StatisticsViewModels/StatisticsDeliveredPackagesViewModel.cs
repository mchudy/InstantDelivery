using Caliburn.Micro;
using InstantDelivery.Services;
using System.Threading.Tasks;

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

        public BindableCollection<Population> Budget { get; } = new BindableCollection<Population>();

        private async void GenerateChart()
        {
            await Task.Run(() =>
            {
                var valueOfPackages = service.ValueOfAllPackages();
                var employeesSalaries = service.EmployeesSalaries();
                var taxes = service.Taxes(valueOfPackages, employeesSalaries);
                Budget.Add(new Population() { Name = "Wartość dostarczanych paczek", Count = valueOfPackages });
                Budget.Add(new Population() { Name = "Pensje pracowników", Count = employeesSalaries });
                Budget.Add(new Population() { Name = "Podatki", Count = taxes });
            });
        }
    }
}