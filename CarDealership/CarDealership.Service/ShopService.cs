using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Model;
using CarDealership.Service.Common;
using CarDealership.Repository;

namespace CarDealership.Service
{
    public class ShopService : IShopService
    {
        private static ShopRepository shopRepository = new ShopRepository();
        public bool Delete(Guid id)
        {
            return shopRepository.Delete(id);
        }

        public List<Shop> Get()
        {
            return shopRepository.Get();
        }

        public Shop Get(Guid id)
        {
            return shopRepository.Get(id);
        }

        public bool Post(Shop shop)
        {
            return shopRepository.Post(shop);
        }


        public bool UpdateShopName(Guid id, Shop shop)
        {
            return shopRepository.UpdateShopName(id, shop);
        }
    }
}
