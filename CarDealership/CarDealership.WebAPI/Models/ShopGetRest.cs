using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarDealership.Model;
using CarDealership.Model.Common;

namespace CarDealership.WebAPI.Models
{
    public class ShopGetRest
    {
        public Guid ShopId { get; set; }
        public string ShopName { get; set; }
        public IAdress ShopAdress { get; set; }


        public static ShopGetRest MapToShopGetRest(Shop shop)
        {
            return new ShopGetRest
            {
                ShopName = shop.ShopName,
                ShopId = shop.ShopId,
                ShopAdress = shop.ShopAdress
            };
        }
        public Shop MapToShop()
        {
            return new Shop
            {
                ShopAdress = this.ShopAdress,
                ShopId = this.ShopId,
                ShopName = this.ShopName
            };
        }

      
    }
}