using Caliburn.Micro;
using InstantDelivery.ViewModel.Proxies;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    ///  Model widoku statystyk budżetowych.
    /// </summary>
    public class FinancialStatisticsViewModel : Screen
    {
        private readonly StatisticsServiceProxy service;

        /// <summary>
        /// Konstruktor modelu widoku.
        /// </summary>
        /// <param name="service"></param>
        public FinancialStatisticsViewModel(StatisticsServiceProxy service)
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
            var statistics = await service.FinancialStatistics();
            Budget.Add(new Population { Name = "Wartość dostarczanych paczek", Count = (int)statistics.TotalPackagesValue });
            Budget.Add(new Population { Name = "Pensje pracowników", Count = (int)statistics.TotalEmployeesSalaries });
            Budget.Add(new Population { Name = "Podatki", Count = (int)statistics.TotalTaxes });
        }
    }
}