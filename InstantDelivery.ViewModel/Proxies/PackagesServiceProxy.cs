using InstantDelivery.Model;
using InstantDelivery.ViewModel.Extensions;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using InstantDelivery.Model.Employees;
using InstantDelivery.Model.Packages;
using InstantDelivery.Model.Paging;

namespace InstantDelivery.ViewModel.Proxies
{
    public class PackagesServiceProxy : ServiceProxyBase
    {
        public PackagesServiceProxy() : base("Packages")
        {
        }

        public async Task<PagedResult<PackageDto>> Page(PageQuery query)
        {
            string queryString = "PageWithSpecifiedEmployee?" + query.ToQueryString();
            return await Get<PagedResult<PackageDto>>(queryString);
        }

        public async Task<PagedResult<PackageDto>> PageWithSpecifiedEmployee(PageQuery query)
        {
            string queryString = "PageWithSpecifiedEmployee?" + query.ToQueryString();
            return await Get<PagedResult<PackageDto>>(queryString);
        }

        public async Task<PagedResult<PackageDto>> PageForLoggedEmployee(PageQuery query)
        {
            string queryString = "PageForLoggedEmployee?" + query.ToQueryString();
            return await Get<PagedResult<PackageDto>>(queryString);
        }

        public async Task<EmployeeDto> GetAssignedEmployee(int packageId)
        {
            return await Get<EmployeeDto>($"{packageId}/Employee");
        }

        public async Task<PagedResult<EmployeeDto>> GetAvailableEmployeesPage(int packageId, PageQuery query)
        {
            string queryString = $"AvailableEmployees/Page?packageId={packageId}&{query.ToQueryString()}";
            return await Get<PagedResult<EmployeeDto>>(queryString);
        }

        public async Task<decimal> CalculatePackageCost(PackageDto package)
        {
            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString[nameof(PackageDto.Height)] = package.Height.ToString(CultureInfo.InvariantCulture);
            queryString[nameof(PackageDto.Width)] = package.Width.ToString(CultureInfo.InvariantCulture);
            queryString[nameof(PackageDto.Length)] = package.Length.ToString(CultureInfo.InvariantCulture);
            queryString[nameof(PackageDto.Weight)] = package.Weight.ToString(CultureInfo.InvariantCulture);

            return await Get<decimal>($"Cost?{queryString}");
        }

        public async Task DeletePackage(int id)
        {
            await Delete(id);
        }

        public async Task RegisterPackage(PackageDto newPackage)
        {
            await PostAsJson("Register", newPackage);
        }

        public async Task AssignPackage(int packageId, int employeeId)
        {
            var content = new FormUrlEncodedContent(
                new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("", employeeId.ToString()),
                });
            await Post($"Assign/{packageId}", content);
        }

        public async Task MarkAsDelivered(int packageId)
        {
            var content = new FormUrlEncodedContent(
                new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("", packageId.ToString()),
                });
            await Post("MarkAsDelivered", content);
        }
    }
}
