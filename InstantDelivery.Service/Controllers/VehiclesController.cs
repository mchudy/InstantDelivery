using AutoMapper;
using AutoMapper.QueryableExtensions;
using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Model;
using InstantDelivery.Model.Vehicles;
using InstantDelivery.Service.Paging;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using InstantDelivery.Model.Paging;

namespace InstantDelivery.Service.Controllers
{
    [Authorize]
    [RoutePrefix("api/Vehicles")]
    public class VehiclesController : ApiController
    {
        private InstantDeliveryContext context;

        public VehiclesController(InstantDeliveryContext context)
        {
            this.context = context;
        }

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

        [Route("Models"), HttpGet]
        public IHttpActionResult GetModels()
        {
            return Ok(context.VehicleModels.ToList());
        }

        public IHttpActionResult Post(AddVehicleDto newVehicle)
        {
            var model = context.VehicleModels.Find(newVehicle.VehicleModelId);
            var vehicle = Mapper.Map<Vehicle>(newVehicle);
            vehicle.VehicleModel = model;
            context.Vehicles.Add(vehicle);
            context.SaveChanges();
            //TODO: return 201
            return Ok();
        }

        [Route("Models"), HttpPost]
        public IHttpActionResult PostVehicleModel(AddVehicleModelDto newVehicleModel)
        {
            var vehicleModel = Mapper.Map<VehicleModel>(newVehicleModel);
            context.VehicleModels.Add(vehicleModel);
            context.SaveChanges();
            //TODO: return 201
            return Ok(vehicleModel.Id);
        }

        public IHttpActionResult Put(VehicleDto vehicle)
        {
            var oldVehicle = context.Vehicles.Find(vehicle.Id);
            if (oldVehicle == null)
            {
                return NotFound();
            }
            Mapper.Map(vehicle, oldVehicle);
            context.SaveChanges();
            return Ok();
        }

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
