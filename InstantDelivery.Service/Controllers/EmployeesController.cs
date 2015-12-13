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
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace InstantDelivery.Service.Controllers
{
    /// <summary>
    /// Kontroler pracowników
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Employees")]
    public class EmployeesController : ApiController
    {
        private readonly InstantDeliveryContext context;
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Konstruktor kontrolera
        /// </summary>
        /// <param name="context">Kontekst danych</param>
        /// <param name="userManager">Obiekt menadżera użytkowników</param>
        public EmployeesController(InstantDeliveryContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        /// <summary>
        /// Zwraca dane zalogowanego kuriera
        /// </summary>
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

        /// <summary>
        /// Zwraca stronę z pracownikami.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [Route("Page"), HttpGet]
        public IHttpActionResult GetPage([FromUri] PageQuery query, string firstName = "",
            string lastName = "", string email = "")
        {
            var employees = context.Employees.AsQueryable();
            employees = ApplyFilters(employees, firstName, lastName, email);
            var dtos = employees.ProjectTo<EmployeeDto>();
            var result = PagingHelper.GetPagedResult(dtos, query);
            foreach (var dto in result.PageCollection)
            {
                var user = context.Employees.Find(dto.Id).User;
                if (user != null)
                {
                    dto.Role = (Role)Enum.Parse(typeof(Role), userManager.GetRoles(user.Id).First());
                }
            }
            return Ok(result);
        }

        /// <summary>
        /// Zwraca stronę paczek dla danego pracownika.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [Route("Packages/Page"), HttpGet]
        public IHttpActionResult PackagesPage([FromUri] PageQuery query, string firstName = "",
            string lastName = "", string email = "")
        {
            var employees = GetAllCouriers();
            employees = ApplyFilters(employees, firstName, lastName, email);
            var dtos = employees.Include(e => e.Packages)
                                .ProjectTo<EmployeePackagesDto>();
            return Ok(PagingHelper.GetPagedResult(dtos, query));
        }

        /// <summary>
        /// Zwraca stronę pojazdów.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [Route("Vehicles/Page"), HttpGet]
        public IHttpActionResult VehiclesPage([FromUri] PageQuery query, string firstName = "",
                string lastName = "", string email = "")
        {
            var employees = GetAllCouriers();
            employees = ApplyFilters(employees, firstName, lastName, email);
            var dtos = employees.Include(e => e.Vehicle)
                                .Include(e => e.Vehicle.VehicleModel)
                                .ProjectTo<EmployeeVehicleDto>();
            return Ok(PagingHelper.GetPagedResult(dtos, query));
        }

        /// <summary>
        /// Dodaje pracownika do bazy danych
        /// </summary>
        /// <param name="newEmployee">Nowy pracownik</param>
        public IHttpActionResult Post(EmployeeAddDto newEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Employee employee = Mapper.Map<Employee>(newEmployee);
            var password = RandomString(15);
            var role = newEmployee.Role;
            var user = new User { UserName = GenerateUserName(employee) };
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

        private IQueryable<Employee> GetAllCouriers()
        {
            var courierRoleId = context.Roles.First(r => r.Name == Role.Courier.ToString()).Id;
            var employees = context.Employees
                    .Where(e => e.User.Roles.Any(r => r.RoleId == courierRoleId))
                    .AsQueryable();
            return employees;
        }

        private static string RandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return res.ToString();
        }

        private string GenerateUserName(Employee employee)
        {
            string username = (employee.FirstName + employee.LastName).ToLower();
            username = ReplaceNationalCharacters(username);
            if (context.Users.Any(u => u.UserName == username))
            {
                int i = 1;
                while (context.Users.Any(u => u.UserName == username + i))
                {
                    i++;
                }
                username = username + i;
            }
            return username;
        }

        private static string ReplaceNationalCharacters(string s)
        {
            StringBuilder sb = new StringBuilder(s);
            sb.Replace('ą', 'a')
              .Replace('ć', 'c')
              .Replace('ę', 'e')
              .Replace('ł', 'l')
              .Replace('ń', 'n')
              .Replace('ó', 'o')
              .Replace('ś', 's')
              .Replace('ż', 'z')
              .Replace('ź', 'z')
              .Replace('Ą', 'A')
              .Replace('Ć', 'C')
              .Replace('Ę', 'E')
              .Replace('Ł', 'L')
              .Replace('Ń', 'N')
              .Replace('Ó', 'O')
              .Replace('Ś', 'S')
              .Replace('Ż', 'Z')
              .Replace('Ź', 'Z');
            return sb.ToString();
        }
    }
}
