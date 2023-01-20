using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Model.Common
{
    public interface IEmployee
    {
        string FirstName { get; set; }
        string Lastname { get; set; }
        Guid PersonId { get; set; }
        string Postition { get; set; }
        List<IShop> Shops { get; set; }
    }
}
