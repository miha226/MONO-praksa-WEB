using Autofac;
using CarDealership.Repository.Common;

namespace CarDealership.Repository
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CarRepository>().As<ICarRepository>();
            builder.RegisterType<ShopRepository>().As<IShopRepository>();
            builder.RegisterType<AdressRepository>().As<IAdressRepository>();
        }
    }
}
