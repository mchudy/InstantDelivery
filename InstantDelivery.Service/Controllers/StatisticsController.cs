using InstantDelivery.Domain;
using InstantDelivery.Model.Statistics;
using System.Linq;
using System.Web.Http;

namespace InstantDelivery.Service.Controllers
{
    /// <summary>
    /// Kontroler statystyk
    /// </summary>
    [Authorize]
    [RoutePrefix("Statistics")]
    public class StatisticsController : ApiController
    {
        private const decimal packageTax = 0.25m;
        private const decimal salaryTax = 0.4m;

        private InstantDeliveryContext context;

        /// <summary>
        /// Konstruktor kontrolera
        /// </summary>
        /// <param name="context"></param>
        public StatisticsController(InstantDeliveryContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Zwraca obiekt statystyk finansowych.
        /// </summary>
        /// <returns></returns>
        [Route("Finance"), HttpGet]
        public IHttpActionResult Finance()
        {
            decimal totalPackagesValue = context.Packages.Sum(p => p.Cost);
            decimal totalSalaries = context.Employees.Sum(e => e.Salary);
            decimal taxes = Taxes(totalPackagesValue, totalSalaries);
            var statistics = new FinancialStatisticsDto
            {
                TotalPackagesValue = totalPackagesValue,
                TotalTaxes = taxes,
                TotalEmployeesSalaries = totalSalaries
            };
            return Ok(statistics);
        }

        /// <summary>
        /// Zwraca obiekt statystyk ogólnych.
        /// </summary>
        /// <returns></returns>
        [Route("General"), HttpGet]
        public IHttpActionResult General()
        {
            var statistics = new GeneralStatisticsDto
            {
                AllPackagesCount = context.Packages.Count(),
                EmployeesCount = context.Employees.Count(),
                AllVehiclesCount = context.Vehicles.Count(),
                AssignedPackages = PackagesWithEmployeeCount(),
                UnassignedPackages = PackagesWithoutEmployeeCount(),
                UnusedVehicles = UnusedVehiclesCount(),
                UsedVehicles = UsedVehiclesCount()
            };
            return Ok(statistics);
        }

        private decimal Taxes(decimal valueOfPackages, decimal employeesSalaries)
        {
            return valueOfPackages * packageTax + employeesSalaries * salaryTax;
        }

        private int PackagesWithEmployeeCount()
        {
            return context.Packages.Count(p => context.Employees
                    .Count(e => e.Packages.Any(x => x.Id == p.Id)) == 1);
        }

        private int PackagesWithoutEmployeeCount()
        {
            return context.Packages
                .Count(p => context.Employees.Count(e => e.Packages.Any(x => x.Id == p.Id)) == 0);
        }

        private int UsedVehiclesCount()
        {
            return context.Vehicles
                .Count(p => context.Employees.Count(e => e.Vehicle.Id == p.Id) == 1);
        }

        private int UnusedVehiclesCount()
        {
            return context.Vehicles
                .Count(p => context.Employees.Count(e => e.Vehicle.Id == p.Id) == 0);
        }
    }
}
