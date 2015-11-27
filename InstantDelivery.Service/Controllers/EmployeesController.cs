using AutoMapper.QueryableExtensions;
using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Model;
using InstantDelivery.Service.Paging;
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
        /// Zwraca stronę pracowników
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route("Page"), HttpPost]
        public IHttpActionResult Page(PageQuery<EmployeeDto> query)
        {
            var dtos = context.Employees.ProjectTo<EmployeeDto>();
            return Ok(PagingHelper.GetPagedResult(dtos, query));
        }

        /// <summary>
        /// Dodaje pracownika do bazy danych
        /// </summary>
        /// <param name="newEmployee">Nowy pracownik</param>
        public IHttpActionResult Post(Employee newEmployee)
        {
            context.Employees.Add(newEmployee);
            context.SaveChanges();
            return Ok(newEmployee.Id);
        }

        /// <summary>
        /// Usuwa pracownika z bazy danych
        /// </summary>
        /// <param name="employeeId">Id pracownika</param>
        public IHttpActionResult Delete(int employeeId)
        {
            var employee = context.Employees.Find(employeeId);
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
