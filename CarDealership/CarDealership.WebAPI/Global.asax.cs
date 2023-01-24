using Autofac;
using Autofac.Integration.WebApi;
using CarDealership.Repository;
using CarDealership.Repository.Common;
using CarDealership.Service;
using CarDealership.Service.Common;
using System.Reflection;
using System.Web.Http;

namespace CarDealership.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<CarRepository>().As<ICarRepository>();
            builder.RegisterType<CarService>().As<ICarService>();

            builder.RegisterType<ShopRepository>().As<IShopRepository>();
            builder.RegisterType<ShopService>().As<IShopService>();

            builder.RegisterType<AdressRepository>().As<IAdressRepository>();
            builder.RegisterType<AdressService>().As<IAdressService>();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
