using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Model.Common;

namespace CarDealership.Model
{
    public class Shop : IShop
    {
        public Guid ShopId { get; set; }
        public string ShopName { get; set; }
        public IAdress ShopAdress { get; set; }
    }
}
