using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CarDealership.Model;

namespace CarDealership.WebAPI.Models
{
    public class CarPostRest
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Shop id is required")]
        public Guid StoredInShop { get; set; }

        [Required(ErrorMessage ="Manufacturer name is required")]
        public string ManufacturerName { get; set; }

        [Required(ErrorMessage ="Model is required")]
        public string Model { get; set; }

        [Required(ErrorMessage ="Date is required")]
        public DateTime? Year { get; set; }

        [Required(ErrorMessage ="Top speed is required")]
        public int TopSpeed { get; set; }
        [Required(ErrorMessage ="Color is required")]
        public string Color { get; set; }
        [Required(ErrorMessage ="Kilometers traveled is required")]
        public int KilometersTraveled { get; set; }

        public static CarPostRest MapToCarRest(Car car)
        {
            return new CarPostRest
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