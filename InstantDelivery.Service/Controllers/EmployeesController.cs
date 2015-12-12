using AutoMapper;
using AutoMapper.QueryableExtensions;
using InstantDelivery.Common.Enums;
using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Model.Employees;
using InstantDelivery.Model.Paging;
using InstantDelivery.Service.Helpers;
using InstantDelivery.Service.Paging;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace InstantDelivery.Service.Controllers
{
    [Authorize]
    [RoutePrefix("api/Employees")]
    public class EmployeesController : ApiController
    {
        private readonly InstantDeliveryContext context;
        private readonly UserManager<User, string> userManager;
        public EmployeesController(InstantDeliveryContext context, UserManager<User, string> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        /// <summary>
        /// Zwraca dane zalogowanego kuriera
        /// </summary>
        /// <param name="id">Id kuriera</param>
        /// <returns>Dane kuriera</returns>
        [Authorize]
        [Route("LoggedCourierData"), HttpGet]
        public IHttpActionResult GetLoggedCourierData()
        {
            var id = User.Identity.GetUserId();
            var employee = context.Employees.FirstOrDefault(e => e.User.Id == id);
            var result = new EmployeeDto();
            Mapper.Map(employee, result, typeof(Employee), typeof(EmployeeDto));
            return Ok(result);
        }

        /// <summary>
        /// Zwraca dane pracownika o podanym identyfikatorze
        /// </summary>
        /// <param name="id">Id pracownika</param>
        /// <returns>Dane pracownika</returns>
        public IHttpActionResult Get(int id)
        {
            var employee = context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            var result = new EmployeeDto();
            Mapper.Map(employee, result, typeof(Employee), typeof(EmployeeDto));
            return Ok(result);
        }

        [Route("Page"), HttpGet]
        public IHttpActionResult GetPage([FromUri] PageQuery query, string firstName = "",
            string lastName = "", string email = "")
        {
            var employees = context.Employees.AsQueryable();
            employees = ApplyFilters(employees, firstName, lastName, email);
            var dtos = employees.ProjectTo<EmployeeDto>();
            return Ok(PagingHelper.GetPagedResult(dtos, query));
        }

        [Route("Packages/Page"), HttpGet]
        public IHttpActionResult PackagesPage([FromUri] PageQuery query, string firstName = "",
            string lastName = "", string email = "")
        {
            var employees = context.Employees.AsQueryable();
            employees = ApplyFilters(employees, firstName, lastName, email);
            var dtos = employees.Include(e => e.Packages)
                                .ProjectTo<EmployeePackagesDto>();
            return Ok(PagingHelper.GetPagedResult(dtos, query));
        }

        [Route("Vehicles/Page"), HttpGet]
        public IHttpActionResult VehiclesPage([FromUri] PageQuery query, string firstName = "",
                string lastName = "", string email = "")
        {
            var employees = context.Employees.AsQueryable();
            employees = ApplyFilters(employees, firstName, lastName, email);
            var dtos = employees.Include(e => e.Vehicle)
                                .Include(e => e.Vehicle.VehicleModel)
                                .ProjectTo<EmployeeVehicleDto>();
            return Ok(PagingHelper.GetPagedResult(dtos, query));
        }

        private static string RandomString(int Size)
        {
            var random = new Random();
            string input = "abcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, Size)
                                   .Select(x => input[random.Next(0, input.Length)]);
            return new string(chars.ToArray());
        }

        /// <summary>
        /// Dodaje pracownika do bazy danych
        /// </summary>
        /// <param name="newEmployee">Nowy pracownik</param>
        public IHttpActionResult Post(EmployeeAddDto newEmployee)
        {
            Employee employee = Mapper.Map<Employee>(newEmployee);

            // not sure what about the role
            var password = RandomString(15);
            var role = Role.Courier;
            var user = new User { UserName = employee.LastName + employee.FirstName };
            if (context.Users.Any(u => user.UserName == u.UserName))
            {
                return BadRequest();
            }
            var result = userManager.Create(user, password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.ToString());
            }
            userManager.AddToRole(user.Id, role.ToString());
            employee.User = user;
            context.Employees.Add(employee);
            context.SaveChanges();
            using (var eh = new EMailHelper())
            {
                eh.SendEmail(employee.Email, "Instant Delivery - Rejestracja", eh.RegistrationBody(employee, password));
            }
            return Ok(employee.Id);
        }

        /// <summary>
        /// Zmienia dane pracownika
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public IHttpActionResult Put(EmployeeDto employee)
        {
            var oldEmployee = context.Employees.Find(employee.Id);
            if (oldEmployee == null)
            {
                return NotFound();
            }
            Mapper.Map(employee, oldEmployee);
            context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Usuwa pracownika z bazy danych
        /// </summary>
        /// <param name="id">Id pracownika</param>
        public IHttpActionResult Delete(int id)
        {
            var employee = context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            foreach (var package in employee.Packages
                        .Where(p => p.Status == PackageStatus.InDelivery))
            {
                package.Status = PackageStatus.New;
            }
            context.Employees.Remove(employee);
            context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Zmienia pojazd przypisany do pracownika
        /// </summary>
        /// <param name="employeeId">Id pracownika</param>
        /// <param name="vehicleId">Id pojazdu</param>
        [Route("ChangeVehicle"), HttpPost]
        public IHttpActionResult ChangeVehicle(int employeeId, int? vehicleId = null)
        {
            var owner = context.Employees.Include(e => e.Vehicle)
                                         .FirstOrDefault(e => e.Id == employeeId);
            if (owner == null)
            {
                return NotFound();
            }
            else
            {
                var vehicle = context.Vehicles.Find(vehicleId);
                owner.Vehicle = vehicle;
                context.SaveChanges();
                return Ok();
            }
        }

        private IQueryable<Employee> ApplyFilters(IQueryable<Employee> source, string firstName,
            string lastName, string email)
        {
            var result = source;
            if (!string.IsNullOrEmpty(firstName))
            {
                result = result.Where(e => e.FirstName.StartsWith(firstName));
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                result = result.Where(e => e.LastName.StartsWith(lastName));
            }
            if (!string.IsNullOrEmpty(email))
            {
                result = result.Where(e => e.Email.StartsWith(email));
            }
            return result;
        }
    }
}
