using CarDealership.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
namespace CarDealership.WebAPI.Controllers
{
    
    public class CarController : ApiController
    {


        public static DataHolder dataHolder = new DataHolder();
        
        // GET: api/Car
        public HttpResponseMessage Get()
        {
            //List<Car> cars = new List<Car>();
            //SqlConnection connection = new SqlConnection(@"Data Source=st-02\SQLEXPRESS;Initial Catalog=CarDealership;Integrated Security=True");
            //SqlCommand command = new SqlCommand("Select * from Car", connection);
            //connection.Open();

            //SqlDataReader reader = command.ExecuteReader();

            //while (reader.Read())
            //{
            //    return Request.CreateResponse(HttpStatusCode.OK, reader["Color"]);
            //}

            List<Car> cars = dataHolder.GetCars();
            if (cars.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, cars);
            }
            
            

            return Request.CreateResponse(HttpStatusCode.NotFound, "There are no cars saved");
        }

        // GET: api/Car/5
        public HttpResponseMessage Get(Guid id)
        {
            Car car = dataHolder.GetCar(id);
            if (car != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, car);
            }
            

            return Request.CreateResponse(HttpStatusCode.NotFound, "There is no element with id "+id);
            
        }

        // POST: api/Car
        public HttpResponseMessage Post(Car car)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "The object is incorrectly defined");
            }
                

            dataHolder.AddCar(new Car() { 
                ManufacturerName=car.ManufacturerName,
                Model=car.Model,
                Year=car.Year,
                Color=car.Color,
                Id=Guid.NewGuid(),
                KilometersTravelled=car.KilometersTravelled,
                TopSpeed=car.TopSpeed});

            return Request.CreateResponse(HttpStatusCode.Created, "Object is added in databse");
        }

        
        

        
        public HttpResponseMessage Put(Car car)
        {
            if (dataHolder.UpdateCarsKilometersTraveled(car))
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                "successfully changed car");
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "there is no sent car in the list");
        }


        // DELETE: api/Car/5
        public HttpResponseMessage Delete(Guid id)
        {
            if (dataHolder.RemoveCarFromList(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Car with id " + id + " is sucessfuly deleted");
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest,"There is no element with id "+id);
        }

        // DELETE: api/Car/
        public HttpResponseMessage Delete([FromBody]Car car)
        {
            bool completed = dataHolder.RemoveCarFromList(new Car()
            {
                Id = car.Id
            });
            if (completed) {
                return Request.CreateResponse(HttpStatusCode.OK, "Car is deleted");
            } 
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Element could not be found");
        }
    }

   
}
