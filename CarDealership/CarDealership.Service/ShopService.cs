using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarDealership.Model;
using CarDealership.Repository.Common;
using CarDealership.Service.Common;

namespace CarDealership.Service
{
    public class ShopService : IShopService
    {
        private IShopRepository shopRepository;

        public ShopService(IShopRepository shopRepository)
        {
            this.shopRepository = shopRepository;
        }
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
