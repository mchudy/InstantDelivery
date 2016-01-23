using AutoMapper;
using AutoMapper.QueryableExtensions;
using InstantDelivery.Common.Enums;
using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Model.Employees;
using InstantDelivery.Model.Packages;
using InstantDelivery.Model.Paging;
using InstantDelivery.Service.Helpers;
using InstantDelivery.Service.Paging;
using InstantDelivery.Service.Pricing;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace InstantDelivery.Service.Controllers
{
    /// <summary>
    /// Kontroler przesyłek
    /// </summary>
    [Authorize]
    [RoutePrefix("Packages")]
    public class PackagesController : ApiController
    {
        private readonly InstantDeliveryContext context;
        private readonly IPricingStrategy pricingStrategy;

        /// <summary>
        /// Konstruktor kontrolera przesyłek.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="pricingStrategy"></param>
        public PackagesController(InstantDeliveryContext context, IPricingStrategy pricingStrategy)
        {
            this.context = context;
            this.pricingStrategy = pricingStrategy;
        }

        /// <summary>
        /// Zwraca paczkę o danym ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            var package = context.Packages.Find(id);
            if (package == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<PackageDto>(package));
        }

        [AllowAnonymous]
        [Route("{id}/History"), HttpGet]
        public IHttpActionResult GetHistory(int id)
        {
            var events = context.PackageEvents
                                .Where(pe => pe.Package.Id == id)
                                .OrderByDescending(pe => pe.Date)
                                .ProjectTo<PackageEventDto>();
            if (!events.Any())
            {
                return NotFound();
            }
            return Ok(events);
        }

        /// <summary>
        /// Zwraca stronę przesyłek.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [Route("Employee/Page"), HttpGet]
        public IHttpActionResult GetPage([FromUri] PageQuery query, string id = "",
            PackageStatusFilter status = PackageStatusFilter.All, string employeeId = "")
        {
            var packages = context.Set<Package>().AsQueryable();

            if (!string.IsNullOrEmpty(employeeId))
            {
                int numericEmployeeId;
                if (int.TryParse(employeeId, out numericEmployeeId))
                {
                    var employee = context.Employees.Find(numericEmployeeId);
                    if (employee != null)
                    {
                        packages = employee.Packages.AsQueryable();
                    }
                }
            }
            packages = ApplyFilters(packages, id, status);
            var dtos = packages.ProjectTo<PackageDto>();
            return Ok(PagingHelper.GetPagedResult(dtos, query));
        }

        /// <summary>
        /// Zwraca stronę paczek zalogowanego użytkownika.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [Route("LoggedEmployee/Page"), HttpGet]
        public IHttpActionResult GetPageForLoggedEmployee([FromUri] PageQuery query, string id = "",
            PackageStatusFilter status = PackageStatusFilter.All, string employeeId = "")
        {
            var packages = context.Set<Package>().AsQueryable();

            var userId = User.Identity.GetUserId();
            var employee = context.Employees.AsQueryable().FirstOrDefault(e => e.User.Id == userId);
            if (employee != null)
            {
                packages = employee.Packages.AsQueryable();
            }

            packages = ApplyFilters(packages, id, status);
            var dtos = packages.ProjectTo<PackageDto>();
            return Ok(PagingHelper.GetPagedResult(dtos, query));
        }

        /// <summary>
        /// Rejestruje paczkę w bazie danych.
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        [Route("Register"), HttpPost]
        public IHttpActionResult RegisterPackage(PackageDto package)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            package.Status = PackageStatus.InWarehouse;
            package.Cost = pricingStrategy.GetCost(package);
            var newPackage = Mapper.Map<Package>(package);
            context.Packages.Add(newPackage);
            context.PackageEvents.Add(new PackageEvent
            {
                EventType = PackageEventType.RegisteredInWarehouse,
                Package = newPackage
            });
            context.SaveChanges();
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
            if (owner == null)
            {
                return BadRequest();
            }
            context.PackageEvents.Add(new PackageEvent
            {
                Package = package,
                Employee = owner,
                EventType = PackageEventType.Delivered
            });
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
        public IHttpActionResult AssignPackage([FromUri]int packageId, [FromBody]int employeeId)
        {
            var package = context.Packages.Find(packageId);
            var employee = context.Employees.Find(employeeId);
            package.Status = PackageStatus.InDelivery;
            employee.Packages.Add(package);
            context.PackageEvents.Add(new PackageEvent
            {
                Employee = employee,
                Package = package,
                EventType = PackageEventType.HandedToCourier
            });
            context.SaveChanges();
            using (var eh = new EMailHelper())
            {
                eh.SendEmail(employee.Email, "Instant Delivery - Nowe zlecenie", eh.AssignedPackageBody(employee));
            }
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
            if (employee == null)
            {
                return Ok();
            }
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
                                e => e.Vehicle != null &&
                                    (double)(e.Packages.Where(p => p.Id != packageId).Sum(p => p.Weight) + package.Weight) <
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

        /// <summary>
        /// Uusuwa paczkę o zadanym ID z bazy danych.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Odłącza paczkę od zalogowanego kuriera.
        /// </summary>
        /// <param name="packageId">ID paczki</param>
        /// <returns></returns>
        [Authorize]
        [Route("DetachPackageFromEmployee"), HttpPost]
        public IHttpActionResult DetachPackageFromEmployee([FromBody] int packageId)
        {
            var package = context.Packages.Find(packageId);
            if (package == null)
            {
                return NotFound();
            }
            var employee = context.Employees.AsQueryable().FirstOrDefault(e => e.Packages.FirstOrDefault(p => p.Id == packageId) != null);
            if (employee == null)
            {
                return NotFound();
            }
            employee.Packages.Remove(package);
            package.Status = PackageStatus.NoticeLeft;
            context.PackageEvents.Add(new PackageEvent
            {
                Employee = employee,
                Package = package,
                EventType = PackageEventType.NoticeLeft
            });
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
                result = result.Where(p => p.Status == PackageStatus.InDelivery || p.Status == PackageStatus.NoticeLeft);
            }
            else if (status == PackageStatusFilter.Delivered)
            {
                result = result.Where(p => p.Status == PackageStatus.Delivered);
            }
            else if (status == PackageStatusFilter.New)
            {
                result = result.Where(p => p.Status == PackageStatus.InWarehouse || p.Status==PackageStatus.InClient);
            }
            else if (status == PackageStatusFilter.ToPickUp)
            {
                result = result.Where(p => p.Status == PackageStatus.ToPickUp);
            }
            return result;
        }
    }
}
