using AutoMapper;
using InstantDelivery.Domain;
using InstantDelivery.Model;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace InstantDelivery.Service.Controllers
{
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

        [Route("Details/{id}")]
        public IHttpActionResult GetDetails(int id)
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
    }
}
