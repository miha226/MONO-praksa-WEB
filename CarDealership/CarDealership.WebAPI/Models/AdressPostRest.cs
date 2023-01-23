using CarDealership.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarDealership.WebAPI.Models
{
    public class AdressPostRest
    {
        [Required]
        public int PostNumber { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }


        public Adress MapToAdress()
        {
            return new Adress
            {
                City = this.City,
                PostNumber = this.PostNumber,
                Street = this.Street
            };
        } 
    }
}