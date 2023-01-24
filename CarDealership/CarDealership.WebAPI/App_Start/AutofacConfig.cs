using Autofac;
using Autofac.Integration.WebApi;
using CarDealership.Repository;
using CarDealership.Service;
using System.Reflection;
using System.Web.Http;

namespace CarDealership.WebAPI.App_Start
{
    public class AutofacConfig
    {
        public static void StartDI()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}