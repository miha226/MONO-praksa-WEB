using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Common
{
    public class Sorting
    {
        public string OrderName { get; set; } = "Model";
        public string OrderDirection { get; set; } = "DESC";

        public bool CheckIfPropertyNameExists<T>(T value )
        {
            foreach (var prop in value.GetType().GetProperties().ToList())
            {
                if (prop.Name == this.OrderName)
                {
                    return true;
                }
            }
            return false;
        }

        public bool OrderDirectionIsValid()
        {
            if (this.OrderDirection == "DESC" || this.OrderDirection == "ASC")
            {
                return true;
            }
            return false;
        }
    }
}
