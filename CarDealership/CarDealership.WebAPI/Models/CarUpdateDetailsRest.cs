using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarDealership.Model;

namespace CarDealership.WebAPI.Models
{
    public class CarUpdateDetailsRest
    {
        public int TopSpeed { get; set; }
        public string Color { get; set; }
        public int KilometersTraveled { get; set; }

        public Car MapToCar()
        {
            return new Car
            {
                TopSpeed = this.TopSpeed,
                Color = this.Color,
                KilometersTraveled = this.KilometersTraveled
            };
        }
    }
}