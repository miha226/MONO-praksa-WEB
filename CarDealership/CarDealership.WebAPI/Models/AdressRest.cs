using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarDealership.Model;

namespace CarDealership.WebAPI.Models
{
    public class AdressRest
    {
        public Guid AdressId { get; set; }
        public int PostNumber { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public static AdressRest MapToAdressRest(Adress adress)
        {
            return new AdressRest()
            {
                AdressId = adress.AdressId,
                City = adress.City,
                PostNumber = adress.PostNumber,
                Street = adress.Street
            };
        }
        public Adress MapToAdress()
        {
            return new Adress()
            {
                Street = this.Street,
                PostNumber = this.PostNumber,
                City = this.City,
                AdressId = this.AdressId
            };
        }
    }
}