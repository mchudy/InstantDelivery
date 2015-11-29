using AutoMapper;
using AutoMapper.QueryableExtensions;
using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Model;
using InstantDelivery.Service.Paging;
using System.Linq;
using System.Web.Http;

namespace InstantDelivery.Service.Controllers
{
    [RoutePrefix("api/Packages")]
    public class PackagesController : ApiController
    {
        private InstantDeliveryContext context;

        public PackagesController(InstantDeliveryContext context)
        {
            this.context = context;
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

        [Route("Page"), HttpGet]
        public IHttpActionResult GetPage([FromUri] PageQuery query, string id = "")
        {
            var packages = context.Packages.AsQueryable();
            packages = ApplyFilters(packages, id);
            var dtos = packages.ProjectTo<PackageDto>();
            return Ok(PagingHelper.GetPagedResult(dtos, query));
        }

        private IQueryable<Package> ApplyFilters(IQueryable<Package> source, string id)
        {
            var result = source;
            if (!string.IsNullOrEmpty(id))
            {
                result = result.Where(p => p.Id.ToString().StartsWith(id));
            }
            return result;
        }
    }
}
