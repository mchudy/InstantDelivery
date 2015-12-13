using InstantDelivery.Model.Statistics;
using InstantDelivery.ViewModel.Dialogs;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel.Proxies
{
    public class StatisticsServiceProxy : ServiceProxyBase
    {
        public StatisticsServiceProxy(IDialogManager dialogManager)
            : base("Statistics", dialogManager)
        {
        }

        /// <summary>
        /// Zwraca obiekt statystyk ogólnych.
        /// </summary>
        /// <returns></returns>
        public async Task<GeneralStatisticsDto> GeneralStatistics()
        {
            return await Get<GeneralStatisticsDto>("General");
        }

        /// <summary>
        /// Zwraca obiekt statystyk finansowych.
        /// </summary>
        /// <returns></returns>
        public async Task<FinancialStatisticsDto> FinancialStatistics()
        {
            return await Get<FinancialStatisticsDto>("Finance");
        }
    }
}
