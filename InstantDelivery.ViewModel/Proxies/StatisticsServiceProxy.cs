using InstantDelivery.Model.Statistics;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel.Proxies
{
    public class StatisticsServiceProxy : ServiceProxyBase
    {
        public StatisticsServiceProxy() : base("Statistics")
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
