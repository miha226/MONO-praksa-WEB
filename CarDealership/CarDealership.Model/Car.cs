using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Model.Common;

namespace CarDealership.Model
{
    public class Car : ICar
    {
        public Guid Id { get; set; }
        public Guid StoredInShop { get; set; }
        public string ManufacturerName { get; set; }
        public string Model { get; set; }
        public DateTime? Year { get; set; }
        public int TopSpeed { get; set; }
        public string Color { get; set; }
        public int KilometersTraveled { get; set; }
    }
}
