using InstantDelivery.Domain.Entities;
using InstantDelivery.Model;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web.Http;

namespace InstantDelivery.Service.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly SignInManager<User, string> signInManager;

        public AccountController(SignInManager<User, string> signInManager)
        {
            this.signInManager = signInManager;
        }

        [Route("Login"), HttpPost]
        public async Task<IHttpActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await signInManager.PasswordSignInAsync(dto.Username, dto.Password,
                isPersistent: false, shouldLockout: false);
            return Ok();
        }
    }
}
