using InstantDelivery.Common.Enums;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace InstantDelivery.Service.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("Roles")]
        public IHttpActionResult GetRoles()
        {
            Role[] roles = ((ClaimsIdentity)User.Identity).Claims
                            .Where(c => c.Type == ClaimTypes.Role)
                            .Select(c => (Role)Enum.Parse(typeof(Role), c.Value))
                            .ToArray();
            return Ok(roles);
        }
    }
}
