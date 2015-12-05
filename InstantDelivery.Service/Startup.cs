using Autofac;
using Autofac.Integration.WebApi;
using InstantDelivery.Domain;
using InstantDelivery.Service.Pricing;
using Microsoft.Owin;
using Owin;
using System.Reflection;
using System.Web.Http;

[assembly: OwinStartup(typeof(InstantDelivery.Service.Startup))]
namespace InstantDelivery.Service
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AutoMapperConfig.RegisterMappings();

            var builder = new ContainerBuilder();
            var config = new HttpConfiguration();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.Register(c => new InstantDeliveryContext())
                .AsSelf()
                .InstancePerRequest();

            builder.Register(c => new RegularPricingStrategy())
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterWebApiFilterProvider(config);

            WebApiConfig.Register(config);

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }
    }
}