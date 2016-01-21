using Autofac;
using Autofac.Integration.WebApi;
using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Service.Pricing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(InstantDelivery.Service.Startup))]
namespace InstantDelivery.Service
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            var builder = new ContainerBuilder();
            var config = new HttpConfiguration();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<InstantDeliveryContext>()
                .AsSelf()
                .As<DbContext>() // for UserStore
                .InstancePerLifetimeScope();

            builder.Register(c => new RegularPricingStrategy())
                .AsImplementedInterfaces()
                .SingleInstance();

            RegisterIdentity(app, builder);

            builder.RegisterWebApiFilterProvider(config);

            WebApiConfig.Register(config);

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);

            // has to be called before UseWebApi
            ConfigureOAuth(app);

            app.UseWebApi(config);
        }

        [SuppressMessage("ReSharper", "RedundantTypeArgumentsOfMethod")]
        private static void RegisterIdentity(IAppBuilder app, ContainerBuilder builder)
        {
            builder.RegisterType<UserStore<User>>()
                .As<IUserStore<User, string>>()
                .As<IUserStore<User>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserManager<User, string>>()
                .AsSelf()
                .InstancePerRequest();

            builder.RegisterType<UserManager<User>>()
                .AsSelf()
                .InstancePerRequest();

            builder.RegisterType<SignInManager<User, string>>()
                .AsSelf()
                .InstancePerRequest();

            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication)
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication)
                .InstancePerRequest();
            builder.Register<IDataProtectionProvider>(c => app.GetDataProtectionProvider())
                .InstancePerRequest();
        }
    }
}