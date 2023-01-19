using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealership.WebAPI.Models
{
    public class Shop
    {
        public Guid ShopId { get; set; }
        public string ShopName { get; set; }
        public Adress ShopAdress { get; set; }
    }
}