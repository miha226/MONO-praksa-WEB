using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Model.Common;

namespace CarDealership.Model
{
    public class Adress : IAdress
    {
        public Guid AdressId { get; set; }
        public int PostNumber { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }
}
