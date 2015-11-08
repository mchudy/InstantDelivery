using Caliburn.Micro;
using InstantDelivery.Services;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    ///  Model widoku statystyk budżetowych.
    /// </summary>
    public class StatisticsDeliveredPackagesViewModel : Screen
    {
        private IStatisticsService service;
        /// <summary>
        /// Konstruktor modelu widoku.
        /// </summary>
        /// <param name="service"></param>
        public StatisticsDeliveredPackagesViewModel(IStatisticsService service)
        {
            this.service = service;
            GenerateChart();
        }
        /// <summary>
        /// Kolekcja skojarzona ze źródłem danych na wykresie.
        /// </summary>
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