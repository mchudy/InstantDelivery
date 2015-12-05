using Autofac;
using Autofac.Integration.WebApi;
using InstantDelivery.Domain;
using System.Reflection;
using System.Web.Http;
using InstantDelivery.Service.Pricing;

namespace InstantDelivery.Service
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

        }
    }
}
