using InstantDelivery.Model;
using System.Threading.Tasks;
using System.Web.Http;

namespace InstantDelivery.Service.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        [Route("Login"), HttpPost]
        public async Task<IHttpActionResult> Login([FromBody] LoginDto dto)
        {
            return Ok();
        }
    }
}
