using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealership.WebAPI.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public Guid StoredInShop { get; set; }
        public string ManufacturerName { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int TopSpeed { get; set; }
        public string Color { get; set; }
        public int KilometersTraveled { get; set; }
        

       
    }
}