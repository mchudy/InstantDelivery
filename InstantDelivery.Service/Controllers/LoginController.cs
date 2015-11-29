using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InstantDelivery.Domain;

namespace InstantDelivery.Service.Controllers
{
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        private InstantDeliveryContext context;

        public LoginController(InstantDeliveryContext context)
        {
            this.context = context;
        }

        [Route("Login"), HttpPost]
        public IHttpActionResult Login(string username, string password)
        {
            // login stuff
            if ( /*zalogowano==*/true)
            {
                return Ok(true);
            }
            else return Ok(false);
        }

        [Route("SendPassword"), HttpPost]
        public IHttpActionResult SendPassword(string email)
        {
            SendMailWithPassword(email);
            return Ok();
        }

        public void SendMailWithPassword(string email)
        { }
        //TODO not sure if here
    }
}
