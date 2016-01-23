using AutoMapper;
using InstantDelivery.Common.Enums;
using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Model.Customers;
using InstantDelivery.Service.Helpers;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace InstantDelivery.Service.Controllers
{
    [Authorize(Roles = "Customer")]
    [RoutePrefix("Customers")]
    public class CustomersController : ApiController
    {
        private readonly InstantDeliveryContext context;
        private readonly UserManager<User, string> userManager;

        public CustomersController(InstantDeliveryContext context, UserManager<User, string> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        /// <summary>
        /// Dodaje nowego klienta
        /// </summary>
        /// <param name="newCustomer">Nowy pracownik</param>
        [AllowAnonymous]
        [Route("Register"), HttpPost]
        public IHttpActionResult Register(CustomerRegisterDto newCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Customer customer = Mapper.Map<Customer>(newCustomer);
            var user = new User { UserName = newCustomer.UserName };
            if (context.Users.Any(u => user.UserName == u.UserName))
            {
                return BadRequest();
            }
            var result = userManager.Create(user, newCustomer.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.ToString());
            }
            userManager.AddToRole(user.Id, Role.Customer.ToString());
            customer.User = user;
            context.Customers.Add(customer);
            context.SaveChanges();
            using (var eh = new EMailHelper())
            {
                //TODO
                //eh.SendEmail(customer.Email, "Instant Delivery - Rejestracja", eh.RegistrationBody(employee, password));
            }
            return Ok();
        }
    }
}
