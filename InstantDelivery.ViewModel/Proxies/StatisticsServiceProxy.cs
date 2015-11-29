using InstantDelivery.Model.Statistics;
using System.Net.Http;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel.Proxies
{
    public class StatisticsServiceProxy : ServiceProxyBase
    {
        public StatisticsServiceProxy() : base("Statistics/")
        {
        }

        public async Task<GeneralStatisticsDto> GeneralStatistics()
        {
            var response = await client.GetAsync("General");
            return await response.Content.ReadAsAsync<GeneralStatisticsDto>();
        }

        public async Task<FinancialStatisticsDto> FinancialStatistics()
        {
            var response = await client.GetAsync("Finance");
            return await response.Content.ReadAsAsync<FinancialStatisticsDto>();
        }
    }
}
