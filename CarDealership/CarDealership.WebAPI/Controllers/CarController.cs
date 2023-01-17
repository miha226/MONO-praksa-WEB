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
        public HttpResponseMessage Get()
        {
            List<Car> cars = dataHolder.GetCars();
            if(dataHolder.GetCars().Any())
            return Request.CreateResponse(HttpStatusCode.OK, cars);

            return Request.CreateResponse(HttpStatusCode.NotFound, "There are no cars saved");
        }

        // GET: api/Car/5
        public HttpResponseMessage Get(int id)
        {
            Car car = dataHolder.GetCar(id);
            if(car!=null)
            return Request.CreateResponse(HttpStatusCode.OK, car);

            return Request.CreateResponse(HttpStatusCode.NotFound, "There is no element with id "+id);
            
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

        
        public HttpResponseMessage Put(int id, [FromBody]int newKilometers)
        {
            if (dataHolder.UpdateCarsKilometersTraveled(id, newKilometers)) return Request.CreateResponse(HttpStatusCode.OK, 
                "successfully changed kilometers on car with id "+id);

            return Request.CreateResponse(HttpStatusCode.NotFound, "There is no car with id " + id);
            
        }

        
        public HttpResponseMessage Put(Car car, int newKilometers)
        {
            if (dataHolder.UpdateCarsKilometersTraveled(car, newKilometers)) return Request.CreateResponse(HttpStatusCode.OK,
                "successfully changed kilometers on car");

            return Request.CreateResponse(HttpStatusCode.NotFound, "there is no sent car in the list");
        }


        // DELETE: api/Car/5
        public HttpResponseMessage Delete(int id)
        {
            if(dataHolder.RemoveCarFromList(id)) return Request.CreateResponse(HttpStatusCode.OK,"Car with id "+id+" is sucessfuly deleted");

            return Request.CreateResponse(HttpStatusCode.BadRequest,"There is no element with id "+id);
        }

        // DELETE: api/Car/
        public HttpResponseMessage Delete([FromBody]Car car)
        {
            bool completed = dataHolder.RemoveCarFromList(new Car()
            {
                Color = car.Color,
                Horsepower = car.Horsepower,
                Id = car.Id,
                KilometersTravelled= car.KilometersTravelled,
                ManufacturerName = car.ManufacturerName,
                Model = car.Model,
                TopSpeed = car.TopSpeed,
                Year = car.Year
            });
            if (completed) return Request.CreateResponse(HttpStatusCode.OK, "Car is deleted");
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Element could not be found");
        }
    }

   
}
