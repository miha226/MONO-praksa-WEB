using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarDealership.Model;
using CarDealership.Service;
using CarDealership.WebAPI.Models;

namespace CarDealership.WebAPI.Controllers
{
    
    public class CarController : ApiController
    {
        public static CarService carService = new CarService();

        // GET: api/Car
        public HttpResponseMessage Get()
        {
            List<Car> cars = carService.Get();
            if (cars.Any())
            {
                List<CarRest> carsRests = new List<CarRest>();
                cars.ForEach(car => carsRests.Add(CarRest.MapToCarRest(car)));
                return Request.CreateResponse(HttpStatusCode.OK, carsRests);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "There are no cars saved");
        }

        // GET: api/Car/5
        public HttpResponseMessage Get(Guid id)
        {

            Car car = carService.Get(id);
            if (car != null) 
            {
                return Request.CreateResponse(HttpStatusCode.OK, CarRest.MapToCarRest(car));
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "There is no element with id "+id);
        }

        // POST: api/Car
        public HttpResponseMessage Post(CarRest car)
        {
            var response = carService.Post(car.MapToCar());
            if (!response.Data)
            { 
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, response.Message);
            }
            return Request.CreateResponse(HttpStatusCode.Created, response.Message);
        }

        
        

        
        public HttpResponseMessage Put(Guid id,CarRest car)
        {
            var response = carService.Put(id, car.MapToCar());
            if (response.Data)
            {
                return Request.CreateResponse(HttpStatusCode.OK, response.Message);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, response.Message);
        }

        [Route("api/Car/carDetails")]
        [HttpPut]
        public HttpResponseMessage UpdateCarDetails(Guid id, CarUpdateDetailsRest car)
        {
            var response = carService.Put(id, car.MapToCar());
            if (response.Data)
            {
                return Request.CreateResponse(HttpStatusCode.OK, response.Message);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, response.Message);
        }


        // DELETE: api/Car/5
        public HttpResponseMessage Delete([FromUri]Guid id)
        {
            var response = carService.Delete(id);
            if (response.Data)
            { 
                return Request.CreateResponse(HttpStatusCode.OK, response.Message);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, response.Message);
        }

       
    }

   
}
