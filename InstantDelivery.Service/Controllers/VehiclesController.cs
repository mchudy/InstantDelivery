using AutoMapper;
using AutoMapper.QueryableExtensions;
using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Model.Paging;
using InstantDelivery.Model.Vehicles;
using InstantDelivery.Service.Paging;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace InstantDelivery.Service.Controllers
{
    /// <summary>
    /// Kontroler pojazdów
    /// </summary>
    [Authorize]
    [RoutePrefix("Vehicles")]
    public class VehiclesController : ApiController
    {
        private InstantDeliveryContext context;

        /// <summary>
        /// Konstruktor kontrolera
        /// </summary>
        /// <param name="context"></param>
        public VehiclesController(InstantDeliveryContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Zwraca pojazd o zadanym ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            var vehicle = context.Vehicles
                .Include(v => v.VehicleModel)
                .SingleOrDefault(v => v.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<VehicleDto>(vehicle));
        }

        /// <summary>
        /// Zwraca stronę pojazdów
        /// </summary>
        /// <param name="query">Filtry</param>
        /// <param name="registrationNumber">Numer rejestracyjny</param>
        /// <param name="model">Model pojazdu</param>
        /// <param name="brand">Marka pojazdu</param>
        /// <returns></returns>
        [Route("Page"), HttpGet]
        public IHttpActionResult GetPage([FromUri] PageQuery query, string registrationNumber = "",
            string model = "", string brand = "")
        {
            var vehicles = context.Vehicles
                .Include(v => v.VehicleModel)
                .AsQueryable();
            vehicles = ApplyFilters(vehicles, registrationNumber, brand, model);
            var dtos = vehicles.ProjectTo<VehicleDto>();
            return Ok(PagingHelper.GetPagedResult(dtos, query));
        }

        /// <summary>
        /// Zwraca wszystkie modele pojazdów
        /// </summary>
        /// <returns></returns>
        [Route("Models"), HttpGet]
        public IHttpActionResult GetModels()
        {
            return Ok(context.VehicleModels.ToList());
        }

        /// <summary>
        /// Dodaje pojazd do bazy danych.
        /// </summary>
        /// <param name="newVehicle"></param>
        /// <returns></returns>
        public IHttpActionResult Post(AddVehicleDto newVehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var model = context.VehicleModels.Find(newVehicle.VehicleModelId);
            var vehicle = Mapper.Map<Vehicle>(newVehicle);
            vehicle.VehicleModel = model;
            context.Vehicles.Add(vehicle);
            context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Dodaje model pojazdu do bazy danych.
        /// </summary>
        /// <param name="newVehicleModel"></param>
        /// <returns></returns>
        [Route("Models"), HttpPost]
        public IHttpActionResult PostVehicleModel(AddVehicleModelDto newVehicleModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var vehicleModel = Mapper.Map<VehicleModel>(newVehicleModel);
            context.VehicleModels.Add(vehicleModel);
            context.SaveChanges();
            return Ok(vehicleModel.Id);
        }
        /// <summary>
        /// Aktualizuje pojazd w bazie danych.
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public IHttpActionResult Put(VehicleDto vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var oldVehicle = context.Vehicles.Find(vehicle.Id);
            if (oldVehicle == null)
            {
                return NotFound();
            }
            Mapper.Map(vehicle, oldVehicle);
            context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Usuwa pojazd o zadanym ID z bazy danych.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            var vehicle = context.Vehicles.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            context.Vehicles.Remove(vehicle);
            context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Zwraca wszystkie dostępne pojazdy dla danego pracownika.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route("Available/Page"), HttpGet]
        public IHttpActionResult GetAllAvailable([FromUri] PageQuery query)
        {
            var vehicles = context.Vehicles
                .Include(v => v.VehicleModel)
                .Where(v => context.Employees.Count(e => e.Vehicle.Id == v.Id) == 0)
                .AsQueryable();
            var dtos = vehicles.ProjectTo<VehicleDto>();
            return Ok(PagingHelper.GetPagedResult(dtos, query));
        }

        private IQueryable<Vehicle> ApplyFilters(IQueryable<Vehicle> source, string registrationNumber, string brand, string model)
        {
            var result = source;
            if (!string.IsNullOrEmpty(registrationNumber))
            {
                result = result.Where(e => e.RegistrationNumber.StartsWith(registrationNumber));
            }
            if (!string.IsNullOrEmpty(brand))
            {
                result = result.Where(e => e.VehicleModel.Brand.StartsWith(brand));
            }
            if (!string.IsNullOrEmpty(model))
            {
                result = result.Where(e => e.VehicleModel.Model.StartsWith(model));
            }
            return result;
        }
    }
}
