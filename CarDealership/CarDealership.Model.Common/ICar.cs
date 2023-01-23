using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Model.Common
{
    public interface ICar
    {
        Guid Id { get; set; }
        Guid StoredInShop { get; set; }
        string ManufacturerName { get; set; }
        string Model { get; set; }
        DateTime? Year { get; set; }
        int TopSpeed { get; set; }
        string Color { get; set; }
        int KilometersTraveled { get; set; }
    }
}
