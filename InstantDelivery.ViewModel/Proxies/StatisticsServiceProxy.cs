using InstantDelivery.Model.Statistics;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel.Proxies
{
    public class StatisticsServiceProxy
    {
        private HttpClient client = new HttpClient();

        public StatisticsServiceProxy()
        {
            client.BaseAddress = new Uri("http://localhost:13600/api/Statistics/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
