using AutoMapper.QueryableExtensions;
using InstantDelivery.Common.Enums;
using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Model;
using InstantDelivery.Model.Paging;
using InstantDelivery.Service.Paging;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace InstantDelivery.Service.Controllers
{
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private InstantDeliveryContext context;
        private readonly UserManager<User, string> userManager;

        public UsersController(InstantDeliveryContext context, UserManager<User, string> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [Route("Page"), HttpGet]
        public IHttpActionResult GetPage([FromUri] PageQuery query)
        {
            var dtos = context.Employees.Include(e => e.User)
                .AsQueryable()
                .Where(e => e.User != null)
                .ProjectTo<UserDto>();
            var result = PagingHelper.GetPagedResult(dtos, query);
            foreach (var dto in result.PageCollection)
            {
                dto.Role = (Role)Enum.Parse(typeof(Role), userManager.GetRoles(dto.Id).First());
            }
            return Ok(result);
        }
    }
}
