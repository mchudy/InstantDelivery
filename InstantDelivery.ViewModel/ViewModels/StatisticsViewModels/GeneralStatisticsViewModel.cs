using Caliburn.Micro;
using InstantDelivery.ViewModel.Proxies;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    ///  Model widoku statystyk ogólnych.
    /// </summary>
    public class GeneralStatisticsViewModel : Screen
    {
        private StatisticsServiceProxy service;

        /// <summary>
        /// Konstruktor modelu widoku
        /// </summary>
        /// <param name="service"></param>
        public GeneralStatisticsViewModel(StatisticsServiceProxy service)
        {
            this.service = service;
            GenerateChart();
        }

        /// <summary>
        /// Kolekcja wartości skojarzona ze źródłem danych wykresu.
        /// </summary>
        public BindableCollection<Population> Values { get; } = new BindableCollection<Population>();

        private async void GenerateChart()
        {
            var statistics = await service.GeneralStatistics();
            Values.Add(new Population { Name = "Liczba pracowników", Count = statistics.EmployeesCount });

            Values.Add(new Population { Name = "Liczba pojazdów", Count = statistics.AllVehiclesCount });
            Values.Add(new Population { Name = "Używane pojazdy", Count = statistics.UsedVehicles });
            Values.Add(new Population { Name = "Nieużywane pojazdy", Count = statistics.UnusedVehicles });

            Values.Add(new Population { Name = "Wszystkie paczki", Count = statistics.AllPackagesCount });
            Values.Add(new Population { Name = "Dostarczane paczki", Count = statistics.AssignedPackages });
            Values.Add(new Population { Name = "Niedostarczane paczki", Count = statistics.UnassignedPackages });
        }
    }
}