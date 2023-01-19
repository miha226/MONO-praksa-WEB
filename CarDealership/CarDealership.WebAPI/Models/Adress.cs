using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealership.WebAPI.Models
{
    public class Adress
    {
        public Guid AdressId { get; set; }
        public int PostNumber { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }
}