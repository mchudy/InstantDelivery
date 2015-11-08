using Autofac;
using InstantDelivery.Core.Entities;
using InstantDelivery.Core.Pricing;
using System.Linq;

namespace InstantDelivery.Core
{
    /// <summary>
    /// Rejestruje klasy z assembly InstantDelivery.Core w kontenerze Autofac
    /// </summary>
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // workaround initializing the context and therefore enabling the async operations
            // (DbContext intialization is not threadsafe)
            using (var context = new InstantDeliveryContext())
            {
                context.Set<Employee>().Any();
            }

            builder.Register<IPricingStrategy>(c => new RegularPricingStrategy())
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.Register(type => new InstantDeliveryContext())
                .AsSelf()
                .InstancePerDependency();
        }
    }
}
