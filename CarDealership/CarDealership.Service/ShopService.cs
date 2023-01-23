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
        public async Task<bool> Delete(Guid id)
        {
            return await shopRepository.Delete(id);
        }

        public async Task<List<Shop>> Get()
        {
            return await shopRepository.Get();
        }

        public async Task<Shop> Get(Guid id)
        {
            return await shopRepository.Get(id);
        }

        public async Task<bool> Post(Shop shop)
        {
            shop.ShopId = Guid.NewGuid();
            return await shopRepository.Post(shop);
        }


        public  async Task<bool> UpdateShopName(Guid id, Shop shop)
        {
            return await shopRepository.UpdateShopName(id, shop);
        }
    }
}
