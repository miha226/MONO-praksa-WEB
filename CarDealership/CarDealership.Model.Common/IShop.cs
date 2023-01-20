using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Model.Common
{
    public interface IShop
    {
        Guid ShopId { get; set; }
        string ShopName { get; set; }
        IAdress ShopAdress { get; set; }
    }
}
