using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Model.Common
{
    public interface IAdress
    {
        Guid AdressId { get; set; }
        int PostNumber { get; set; }
        string City { get; set; }
        string Street { get; set; }
    }
}
