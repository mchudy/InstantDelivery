using AutoMapper;
using AutoMapper.QueryableExtensions;
using InstantDelivery.Common.Enums;
using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Model;
using InstantDelivery.Service.Paging;
using InstantDelivery.Service.Pricing;
using System.Linq;
using System.Web.Http;
using InstantDelivery.Model.Employees;
using InstantDelivery.Model.Packages;
using InstantDelivery.Model.Paging;
using Microsoft.AspNet.Identity;

namespace InstantDelivery.Service.Controllers
{
    [Authorize]
    [RoutePrefix("api/Packages")]
    public class PackagesController : ApiController
    {
        private readonly InstantDeliveryContext context;
        private readonly IPricingStrategy pricingStrategy;

        public PackagesController(InstantDeliveryContext context, IPricingStrategy pricingStrategy)
        {
            this.context = context;
            this.pricingStrategy = pricingStrategy;
        }

        public IHttpActionResult Get(int id)
        {
            var package = context.Packages.Find(id);
            if (package == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<PackageDto>(package));
        }

        [Route("PageWithSpecifiedEmployee"), HttpGet]
        public IHttpActionResult GetPage([FromUri] PageQuery query, string id = "",
            PackageStatusFilter status = PackageStatusFilter.All, string employeeId = "")
        {
            var packages = context.Set<Package>().AsQueryable();

            if (!string.IsNullOrEmpty(employeeId))
            {
                var empId = employeeId;
                var firstOrDefault = context.Employees.AsQueryable().FirstOrDefault(e => e.Id.ToString() == empId);
                if (firstOrDefault != null)
                    packages = firstOrDefault.Packages.AsQueryable();
            }
            packages = ApplyFilters(packages, id, status);
            var dtos = packages.ProjectTo<PackageDto>();
            return Ok(PagingHelper.GetPagedResult(dtos, query));
        }

        [Route("PageForLoggedEmployee"), HttpGet]
        public IHttpActionResult GetPageForLoggedEmployee([FromUri] PageQuery query, string id = "",
            PackageStatusFilter status = PackageStatusFilter.All, string employeeId = "")
        {
            var packages = context.Set<Package>().AsQueryable();

            var userId = User.Identity.GetUserId();
            var empId = context.Employees.AsQueryable().FirstOrDefault(e => e.User.Id == userId);
            var firstOrDefault = context.Employees.AsQueryable().FirstOrDefault(e => e.Id == empId.Id);
            if (firstOrDefault != null)
                packages = firstOrDefault.Packages.AsQueryable();

            packages = ApplyFilters(packages, id, status);
            var dtos = packages.ProjectTo<PackageDto>();
            return Ok(PagingHelper.GetPagedResult(dtos, query));
        }

        [Route("Page"), HttpGet]
        public IHttpActionResult GetPage([FromUri] PageQuery query, string id = "",
            PackageStatusFilter status = PackageStatusFilter.All)
        {
            var packages = context.Set<Package>().AsQueryable();
            packages = ApplyFilters(packages, id, status);
            var dtos = packages.ProjectTo<PackageDto>();
            return Ok(PagingHelper.GetPagedResult(dtos, query));
        }

        [Route("Register"), HttpPost]
        public IHttpActionResult RegisterPackage(PackageDto package)
        {
            package.Status = PackageStatus.New;
            package.Cost = pricingStrategy.GetCost(package);
            var newPackage = Mapper.Map<Package>(package);
            context.Packages.Add(newPackage);
            context.SaveChanges();
            //TODO: return 201
            return Ok();
        }

        /// <summary>
        /// Oznacza paczkę jako dostarczoną i usuwa ją ze zbioru paczek 
        /// dostarczającego pracownika
        /// </summary>
        /// <param name="id">Identyfikator paczki</param>
        [Route("MarkAsDelivered"), HttpPost]
        public IHttpActionResult MarkAsDelivered([FromBody]int id)
        {
            var package = context.Packages.Find(id);
            if (package == null)
            {
                return NotFound();
            }
            package.Status = PackageStatus.Delivered;
            var owner = context.Employees.FirstOrDefault(e => e.Packages.Any(p => p.Id == package.Id));
            if(owner == null)
            {
                return BadRequest();
            }
            owner.Packages.Remove(package);
            context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Przypisuje paczkę do pracownika i zmienia jej status
        /// </summary>
        /// <param name="packageId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [Route("Assign/{packageId:int}"), HttpPost]
        //TODO: error handling
        public IHttpActionResult AssignPackage([FromUri]int packageId, [FromBody]int employeeId)
        {
            var package = context.Packages.Find(packageId);
            var employee = context.Employees.Find(employeeId);
            package.Status = PackageStatus.InDelivery;
            employee.Packages.Add(package);
            context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Zwraca pracownika do którego przypisana jest dana paczka
        /// </summary>
        /// <returns></returns>
        [Route("{id}/Employee"), HttpGet]
        public IHttpActionResult GetAssignedEmployee(int id)
        {
            var employee = context.Employees
                           .FirstOrDefault(e => e.Packages.Count(p => p.Id == id) > 0);
            //TODO: return 404 if not found
            return Ok(Mapper.Map<EmployeeDto>(employee));
        }

        /// <summary>
        /// Zwraca wszytskich pracowników, dla których przypisanie danej paczki
        /// nie przekroczy maksymalnej ładowności samochodu
        /// </summary>
        /// <param name="packageId"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route("AvailableEmployees/Page"), HttpGet]
        public IHttpActionResult GetAvailableEmployeesPage(int packageId, [FromUri] PageQuery query)
        {
            var package = context.Packages.Find(packageId);
            var employees = context.Employees.Where(
                                e => (double)(e.Packages.Where(p => p.Id != packageId).Sum(p => p.Weight) + package.Weight) <
                                    e.Vehicle.VehicleModel.Payload)
                            .ProjectTo<EmployeeDto>();
            return Ok(PagingHelper.GetPagedResult(employees, query));
        }

        /// <summary>
        /// Oblicza koszt paczki na podstawie jej danych
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        [Route("Cost"), HttpGet]
        public IHttpActionResult GetPackageCost([FromUri] PackageDto package)
        {
            return Ok(pricingStrategy.GetCost(package));
        }

        public IHttpActionResult Delete(int id)
        {
            var package = context.Packages.Find(id);
            if (package == null)
            {
                return NotFound();
            }
            context.Packages.Remove(package);
            context.SaveChanges();
            return Ok();
        }

        private IQueryable<Package> ApplyFilters(IQueryable<Package> source, string id, PackageStatusFilter status)
        {
            var result = source;
            if (!string.IsNullOrEmpty(id))
            {
                result = result.Where(p => p.Id.ToString().StartsWith(id));
            }
            if (status == PackageStatusFilter.InDelivery)
            {
                result = result.Where(p => p.Status == PackageStatus.InDelivery);
            }
            else if (status == PackageStatusFilter.Delivered)
            {
                result = result.Where(p => p.Status == PackageStatus.Delivered);
            }
            else if (status == PackageStatusFilter.New)
            {
                result = result.Where(p => p.Status == PackageStatus.New);
            }
            return result;
        }
    }
}
