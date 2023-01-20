using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarDealership.Model;

namespace CarDealership.WebAPI.Models
{
    public class CarRest
    {
        public Guid Id { get; set; }
        public Guid StoredInShop { get; set; }
        public string ManufacturerName { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int TopSpeed { get; set; }
        public string Color { get; set; }
        public int KilometersTraveled { get; set; }

        public static CarRest MapToCarRest(Car car)
        {
            return new CarRest
            {
                Id = car.Id,
                Model = car.Model,
                Color = car.Color,
                KilometersTraveled = car.KilometersTraveled,
                ManufacturerName = car.ManufacturerName,
                StoredInShop = car.StoredInShop,
                TopSpeed = car.TopSpeed,
                Year = car.Year
            };
        }

        public Car MapToCar()
        {
            return new Car
            {
                Year = this.Year,
                TopSpeed = this.TopSpeed,
                StoredInShop = this.StoredInShop,
                Color = this.Color,
                Id = this.Id,
                KilometersTraveled = this.KilometersTraveled,
                ManufacturerName = this.ManufacturerName,
                Model = this.Model
            };
        }

    }
}