using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Common
{
    public class FilterCar
    {
        public DateTime? DateMin { get; set; }
        public DateTime? DateMax { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public string ManufacturerName { get; set; }
        public int? KilometersTraveled { get; set; }
        public int? TopSpeed { get; set; }

        public bool IsNotEmpty()
        {
            if(this.Color != null)
            {
                return true;
            }
            if (this.Model != null)
            {
                return true;
            }
            if (this.ManufacturerName != null)
            {
                return true;
            }
            if (this.KilometersTraveled != null)
            {
                return true;
            }
            if (this.TopSpeed != null)
            {
                return true;
            }
            if (DateNotEmpty())
            {
                return true;
            }
            
            return false;
        }

        public bool DateNotEmpty()
        {
            if (this.DateMin != null && this.DateMax != null)
            {
                return true;
            }
            else if (this.DateMin != null && this.DateMax == null)
            {
                this.DateMax = DateTime.Today;
                return true;
            }
            else if (DateMin == null && DateMax != null)
            {
                this.DateMin = DateTime.Today.AddYears(-70);
                return true;
            }
            return false;
        }

    }
}
