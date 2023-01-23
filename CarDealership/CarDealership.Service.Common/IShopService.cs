using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Model;

namespace CarDealership.Service.Common
{
    public interface IShopService
    {
        Task<bool> UpdateShopName(Guid id, Shop shop);
        Task<List<Shop>> Get();
        Task<Shop> Get(Guid id);
        Task<bool> Delete(Guid id);
        Task<bool> Post(Shop shop);
    }
}
