using InstantDelivery.Common.Enums;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace InstantDelivery.Service.Controllers
{
    /// <summary>
    /// Kontroler kont użytkowników
    /// </summary>
    [Authorize]
    [RoutePrefix("Account")]
    public class AccountController : ApiController
    {
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Konstruktor kontrolera
        /// </summary>
        /// <param name="userManager">Obiekt menadżera użytkowników</param>
        public AccountController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Zwraca role danego użytkownika. 
        /// </summary>
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

        /// <summary>
        /// Zmienia hasło użytkownika
        /// </summary>
        /// <param name="dto"></param>
        [Route("ChangePassword"), HttpPost]
        public async Task<IHttpActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity;
                var result = await userManager.ChangePasswordAsync(user.GetUserId<string>(), dto.CurrentPassword,
                    dto.NewPassword);
                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            return BadRequest(ModelState);
        }
    }
}
