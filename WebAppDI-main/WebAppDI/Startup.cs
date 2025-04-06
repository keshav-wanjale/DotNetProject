using CasCap.Controllers;
using CasCap.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
[assembly: OwinStartup(typeof(CasCap.Startup))]
namespace CasCap
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var resolverMVC = new DefaultDependencyResolverMVC(services.BuildServiceProvider());
            DependencyResolver.SetResolver(resolverMVC);
            var resolverAPI = new DefaultDependencyResolverAPI(services.BuildServiceProvider());
            GlobalConfiguration.Configuration.DependencyResolver = resolverAPI;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //add all MVC/API Controllers in the Web Application project itself
            services.AddControllersAsServices(typeof(Startup).Assembly.GetExportedTypes()
              .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
              .Where(t => typeof(IController).IsAssignableFrom(t)
                 || t.Name.EndsWith(nameof(Controller), StringComparison.OrdinalIgnoreCase)));

            //add additional MVC/API Controllers in the external Class Library project
            services.AddTransient(typeof(StringsController));
            services.AddTransient(typeof(AboutController));
            
            //setup Logging
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            //setup our test DI Service
            services.AddSingleton<IDITestService, DITestService>();
        }
    }

    public class DefaultDependencyResolverMVC : System.Web.Mvc.IDependencyResolver
    {
        protected IServiceProvider serviceProvider;

        public DefaultDependencyResolverMVC(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public object GetService(Type serviceType)
        {
            return this.serviceProvider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.serviceProvider.GetServices(serviceType);
        }
    }

    public class DefaultDependencyResolverAPI : System.Web.Http.Dependencies.IDependencyResolver
    {
        protected IServiceProvider _serviceProvider;

        public DefaultDependencyResolverAPI(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        IDependencyScope System.Web.Http.Dependencies.IDependencyResolver.BeginScope()
        {
            return this;
        }

        public void Dispose() { }

        public object GetService(Type serviceType)
        {
            return this._serviceProvider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this._serviceProvider.GetServices(serviceType);
        }

        public void AddService()
        {

        }
    }
}