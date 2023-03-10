using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarDealership.Model.Common;
using CarDealership.Model;

namespace CarDealership.WebAPI.Models
{
    public class ShopRest
    {
        public Guid ShopId { get; set; }
        public string ShopName { get; set; }
        public Guid ShopAdress { get; set; }


        public static ShopRest MapToShopRest(Shop shop)
        {
            return new ShopRest
            {
                ShopName = shop.ShopName,
                ShopId = shop.ShopId,
                ShopAdress =  shop.ShopAdress.AdressId
            };
        }

        public Shop MapToShop()
        {
            return new Shop
            {
                ShopAdress = new Adress {AdressId= this.ShopAdress },
                ShopId = this.ShopId,
                ShopName = this.ShopName
            };
        }
    }  
}