using Autofac;
using Caliburn.Micro;
using InstantDelivery.Core;
using InstantDelivery.Services;
using InstantDelivery.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using IContainer = Autofac.IContainer;

namespace InstantDelivery
{
    /// <summary>
    /// Klasa tworząca podstawy działania aplikacji
    /// </summary>
    public class Bootstrapper : BootstrapperBase
    {
        private IContainer container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            ConfigureTypeMapping();
            BuildContainer();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[]
            {
                   GetType().Assembly,
                   typeof(ShellViewModel).Assembly,
            };
        }

        protected override object GetInstance(Type service, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return container.Resolve(service);
            }
            else
            {
                return container.ResolveNamed(key, service);
            }
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.Resolve(typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
        }

        protected override void BuildUp(object instance)
        {
            container.InjectProperties(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        private static void ConfigureTypeMapping()
        {
            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViews = "InstantDelivery.Views",
                DefaultSubNamespaceForViewModels = "InstantDelivery.ViewModel"
            };
            ViewLocator.ConfigureTypeMappings(config);
            ViewModelLocator.ConfigureTypeMappings(config);
        }

        private void BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(typeof(ShellViewModel).Assembly)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .Where(type => type.GetInterface(typeof(INotifyPropertyChanged).Name) != null)
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(GetType().Assembly)
                .Where(type => type.Name.EndsWith("View"))
                .AsSelf()
                .InstancePerDependency();

            builder.Register<IWindowManager>(c => new WindowManager())
                .InstancePerLifetimeScope();
            builder.Register<IEventAggregator>(c => new EventAggregator())
                .InstancePerLifetimeScope();

            builder.RegisterModule<CoreModule>();

            builder.RegisterAssemblyTypes(typeof(EmployeeService).Assembly)
                .AsImplementedInterfaces()
                .InstancePerDependency();

            container = builder.Build();
        }
    }
}