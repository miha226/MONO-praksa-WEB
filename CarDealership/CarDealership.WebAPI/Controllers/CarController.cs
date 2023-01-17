using CarDealership.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarDealership.WebAPI.Controllers
{
    
    public class CarController : ApiController
    {


        public static DataHolder dataHolder = new DataHolder();
        // GET: api/Car
        public IEnumerable<Car> Get()
        {
            return dataHolder.GetCars();
        }

        // GET: api/Car/5
        public HttpResponseMessage Get(int id)
        {
            Car car = dataHolder.GetCar(id);
            if(car!=null)
            return Request.CreateResponse(HttpStatusCode.OK, car);
            return Request.CreateResponse(HttpStatusCode.NoContent, "No element with id " + id);
            
        }

        // POST: api/Car
        public IHttpActionResult Post(Car car)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            dataHolder.AddCar(new Car() { 
                ManufacturerName=car.ManufacturerName,
                Model=car.Model,
                Year=car.Year,
                Color=car.Color,
                Horsepower=car.Horsepower,
                Id=car.Id,
                KilometersTravelled=car.KilometersTravelled,
                TopSpeed=car.TopSpeed});

            return Ok();
        }

        
        public void Put(int id, [FromBody]int newKilometers)
        {
            dataHolder.UpdateCarsKilometersTraveled(id, newKilometers);
        }

        
        public void Put(Car car, int newKilometers)
        {
            dataHolder.UpdateCarsKilometersTraveled(car, newKilometers);
        }


        // DELETE: api/Car/5
        public bool Delete(int id)
        {
           return dataHolder.RemoveCarFromList(id);
        }

        // DELETE: api/Car/
        public bool Delete([FromBody]Car car)
        {
           return dataHolder.RemoveCarFromList(car);
        }
    }

   
}
