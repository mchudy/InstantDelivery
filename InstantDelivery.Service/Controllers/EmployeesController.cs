using AutoMapper;
using AutoMapper.QueryableExtensions;
using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Model;
using InstantDelivery.Service.Paging;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace InstantDelivery.Service.Controllers
{
    [RoutePrefix("api/Employees")]
    public class EmployeesController : ApiController
    {
        private readonly InstantDeliveryContext context;

        public EmployeesController(InstantDeliveryContext context)
        {
            this.context = context;
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
            return Ok(Mapper.Map<EmployeeDto>(employee));
        }

        /// <summary>
        /// Zwraca stronę pracowników
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route("Page"), HttpPost]
        public IHttpActionResult Page([FromBody] PageQuery<EmployeeDto> query)
        {
            var dtos = context.Employees.ProjectTo<EmployeeDto>();
            return Ok(PagingHelper.GetPagedResult(dtos, query));
        }

        [Route("Packages/Page"), HttpPost]
        public IHttpActionResult PackagesPage([FromBody] PageQuery<EmployeePackagesDto> query)
        {
            var dtos = context.Employees.Include(e => e.Packages).ProjectTo<EmployeePackagesDto>();
            return Ok(PagingHelper.GetPagedResult(dtos, query));
        }

        /// <summary>
        /// Dodaje pracownika do bazy danych
        /// </summary>
        /// <param name="newEmployee">Nowy pracownik</param>
        public IHttpActionResult Post(EmployeeAddDto newEmployee)
        {
            context.Employees.Add(Mapper.Map<Employee>(newEmployee));
            context.SaveChanges();
            return Ok(newEmployee.Id);
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
        public IHttpActionResult ChangeVehicle(int employeeId, int vehicleId)
        {
            var owner = context.Employees.Find(employeeId);
            var vehicle = context.Vehicles.Find(vehicleId);
            if (owner == null || vehicle == null)
            {
                return NotFound();
            }
            else
            {
                owner.Vehicle = vehicle;
                context.SaveChanges();
                return Ok();
            }
        }
    }
}
