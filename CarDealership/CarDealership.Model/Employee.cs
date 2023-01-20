using CarDealership.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Model
{
    class Employee : IEmployee
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public Guid PersonId { get; set; }
        public string Postition { get; set; }
        public List<IShop> Shops { get; set; }
    }
}
