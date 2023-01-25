using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CarDealership.Common;
using CarDealership.Model;
using CarDealership.Service.Common;
using CarDealership.WebAPI.Models;

namespace CarDealership.WebAPI.Controllers
{
    
    public class CarController : ApiController
    {
        private ICarService carService;

        public CarController(ICarService carService)
        {
            this.carService = carService;
        }

        
        public async Task<HttpResponseMessage> Get([FromUri]Sorting sorting, [FromUri]Paging paging, [FromUri]FilterCar filter)
        {
            List<Car> cars = await carService.GetAsync(sorting, paging, filter);
            if (cars.Any())
            {
                List<CarRest> carsRests = new List<CarRest>();
                cars.ForEach(car => carsRests.Add(CarRest.MapToCarRest(car)));
                return Request.CreateResponse(HttpStatusCode.OK, carsRests);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "There are no cars saved");
        }

        
        public async Task<HttpResponseMessage> Get(Guid id)
        {

            Car car = await carService.Get(id);
            if (car != null) 
            {
                return Request.CreateResponse(HttpStatusCode.OK, CarRest.MapToCarRest(car));
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "There is no element with id "+id);
        }

        
        public async Task<HttpResponseMessage> Post(CarPostRest car)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Car object is incomplete");
            }
            var response = await carService.Post(car.MapToCar());
            if (!response.Data)
            { 
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, response.Message);
            }
            return Request.CreateResponse(HttpStatusCode.Created, response.Message);
        }


        public async Task<HttpResponseMessage> Put(Guid id,CarRest car)
        {
            var response = await carService.Put(id, car.MapToCar());
            if (response.Data)
            {
                return Request.CreateResponse(HttpStatusCode.OK, response.Message);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, response.Message);
        }

        [Route("api/Car/carDetails")]
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateCarDetails(Guid id, CarUpdateDetailsRest car)
        {
            var response = await carService.Put(id, car.MapToCar());
            if (response.Data)
            {
                return Request.CreateResponse(HttpStatusCode.OK, response.Message);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, response.Message);
        }


        
        public async Task<HttpResponseMessage> Delete([FromUri]Guid id)
        {
            var response = await carService.Delete(id);
            if (response.Data)
            { 
                return Request.CreateResponse(HttpStatusCode.OK, response.Message);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, response.Message);
        }

       
    }

   
}
