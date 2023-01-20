using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Model;

namespace CarDealership.Repository.Common
{
    public interface IShopRepository
    {
        bool UpdateShopName(Guid id, Shop shop);
        List<Shop> Get();
        Shop Get(Guid id);
        bool Delete(Guid id);
        bool Post(Shop shop);
    }
}
