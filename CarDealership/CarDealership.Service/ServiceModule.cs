using Autofac;
using CarDealership.Service.Common;

namespace CarDealership.Service
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CarService>().As<ICarService>();
            builder.RegisterType<AdressService>().As<IAdressService>();
            builder.RegisterType<ShopService>().As<IShopService>();
        }
    }
}
