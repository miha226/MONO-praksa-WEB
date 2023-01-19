using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealership.WebAPI.Models
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public Guid PersonId { get; set; }
        public string Postition { get; set; }
        public List<Shop> Shops {get;set;}
    }
}