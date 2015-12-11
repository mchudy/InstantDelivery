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

        public async Task<GeneralStatisticsDto> GeneralStatistics()
        {
            return await Get<GeneralStatisticsDto>("General");
        }

        public async Task<FinancialStatisticsDto> FinancialStatistics()
        {
            return await Get<FinancialStatisticsDto>("Finance");
        }
    }
}
