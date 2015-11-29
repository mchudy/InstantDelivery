using InstantDelivery.Model;
using InstantDelivery.ViewModel.Extensions;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace InstantDelivery.ViewModel.Proxies
{
    public class PackagesServiceProxy : ServiceProxyBase
    {
        public PackagesServiceProxy() : base("Packages/")
        {
        }

        public async Task<Model.PagedResult<PackageDto>> Page(PageQuery query)
        {
            HttpResponseMessage response = await client.GetAsync("Page?" + query.ToQueryString());
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<Model.PagedResult<PackageDto>>();
        }

        public async Task RegisterPackage(PackageDto newPackage)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("Register", newPackage);
            response.EnsureSuccessStatusCode();
        }

        public async Task<decimal> CalculatePackageCost(PackageDto package)
        {
            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString[nameof(PackageDto.Height)] = package.Height.ToString(CultureInfo.InvariantCulture);
            queryString[nameof(PackageDto.Width)] = package.Width.ToString(CultureInfo.InvariantCulture);
            queryString[nameof(PackageDto.Length)] = package.Length.ToString(CultureInfo.InvariantCulture);
            queryString[nameof(PackageDto.Weight)] = package.Weight.ToString(CultureInfo.InvariantCulture);

            HttpResponseMessage response = await client.GetAsync("Cost?" + queryString);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<decimal>();
        }

        public async Task<EmployeeDto> GetAssignedEmployee(int packageId)
        {
            HttpResponseMessage response = await client.GetAsync($"{packageId}/Employee");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<EmployeeDto>();
        }

        public async Task DeletePackage(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(id.ToString());
            response.EnsureSuccessStatusCode();
        }

        public async Task<PagedResult<EmployeeDto>> GetAvailableEmployeesPage(int packageId, PageQuery query)
        {
            HttpResponseMessage response = await client.GetAsync($"AvailableEmployees/Page?packageId={packageId}&" +
                query.ToQueryString());
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<PagedResult<EmployeeDto>>();
        }

        public async Task AssignPackage(int packageId, int employeeId)
        {
            var content = new FormUrlEncodedContent(
                new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("", employeeId.ToString()),
                });
            HttpResponseMessage response = await client.PostAsync($"Assign/{packageId}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task MarkAsDelivered(int packageId)
        {
            var content = new FormUrlEncodedContent(
                new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("", packageId.ToString()),
                });
            HttpResponseMessage response = await client.PostAsync("MarkAsDelivered", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
