using CarDealership.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace CarDealership.WebAPI.Controllers
{
    
    public class CarController : ApiController
    {


        

        public static string connectionString = @"Data Source=st-02\SQLEXPRESS;Initial Catalog=CarDealership;Integrated Security=True";

        // GET: api/Car
        public HttpResponseMessage Get()
        {


            List<Car> cars = new List<Car>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Select * from Car", connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                    cars.Add(new Car()
                    {
                        Color = reader["Color"].ToString(),
                        Id = Guid.Parse(reader["CarId"].ToString()),
                        KilometersTraveled = int.Parse(reader["KilometersTraveled"].ToString()),
                        Year = int.Parse(reader["YearOfManufacture"].ToString()),
                        ManufacturerName = reader["ManufacturerName"].ToString(),
                        Model = reader["Model"].ToString(),
                        TopSpeed = int.Parse(reader["TopSpeed"].ToString()),
                        StoredInShop = Guid.Parse(reader["StoredInShop"].ToString())

                    });
 
            }
            reader.Close();
            connection.Close();
            if (cars.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, cars);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "There are no cars saved");
        }

        // GET: api/Car/5
        public HttpResponseMessage Get(Guid id)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Select * from Car where CarId='"+id+"'", connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Car car = new Car()
                {
                    Color = reader["Color"].ToString(),
                    Id = Guid.Parse(reader["CarId"].ToString()),
                    KilometersTraveled = int.Parse(reader["KilometersTraveled"].ToString()),
                    Year = int.Parse(reader["YearOfManufacture"].ToString()),
                    ManufacturerName = reader["ManufacturerName"].ToString(),
                    Model = reader["Model"].ToString(),
                    TopSpeed = int.Parse(reader["TopSpeed"].ToString()),
                    StoredInShop = Guid.Parse(reader["StoredInShop"].ToString())
                };
                connection.Close();
                return Request.CreateResponse(HttpStatusCode.OK, car);
            }
            connection.Close();
            return Request.CreateResponse(HttpStatusCode.NotFound, "There is no element with id "+id);
            
        }

        // POST: api/Car
        public HttpResponseMessage Post(Car car)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "The object is incorrectly defined");
            }
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Insert into Car (ManufacturerName,Model,YearOfManufacture,KilometersTraveled,TopSpeed,Color,StoredInShop) "+ 
            "values('"+car.ManufacturerName+"', '"+car.Model+"', "+car.Year+", "+car.KilometersTraveled+", "+car.TopSpeed+
            ", '"+car.Color+"', '"+car.StoredInShop+"')", connection);

            connection.Open();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, ex.Message);
                throw;
            }
            
            connection.Close();
            return Request.CreateResponse(HttpStatusCode.Created, "Object is added in databse");
        }

        
        

        
        public HttpResponseMessage Put(Guid id,Car car)
        {
            Car updatedCar=null;
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand("Select * from Car where CarId='" + id + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                updatedCar = new Car()
                {
                    Color = car.Color ?? reader["Color"].ToString(),
                    KilometersTraveled = car.KilometersTraveled == 0 ? int.Parse(reader["KilometersTraveled"].ToString()) : car.KilometersTraveled,
                    Year = car.Year == 0 ? int.Parse(reader["YearOfManufacture"].ToString()) : car.Year,
                    ManufacturerName = car.ManufacturerName ?? reader["ManufacturerName"].ToString(),
                    Model = car.Model ?? reader["Model"].ToString(),
                    TopSpeed = car.TopSpeed==0 ? int.Parse(reader["TopSpeed"].ToString()) : car.TopSpeed
                };
            }
            reader.Close();
            if (updatedCar != null)
            {
                command = new SqlCommand("Update car set Color='" + updatedCar.Color + "', YearOfManufacture=" + updatedCar.Year +
                    ", ManufacturerName='" + updatedCar.ManufacturerName + 
                    "', Model='" + updatedCar.Model + 
                    "', TopSpeed=" + updatedCar.TopSpeed +
                    ", KilometersTraveled=" + updatedCar.KilometersTraveled, connection);
                reader = command.ExecuteReader();
                reader.Close();
                connection.Close();
                return Request.CreateResponse(HttpStatusCode.OK, "Car is updated");
            }
            connection.Close();
            return Request.CreateResponse(HttpStatusCode.NotFound, "there is no sent car in the list");
        }


        // DELETE: api/Car/5
        public HttpResponseMessage Delete([FromUri]Guid id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Select * from Car where CarId='" + id + "'",connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                reader.Close();
                command = new SqlCommand("Delete from Car where CarId='" + id + "'", connection);
                command.ExecuteNonQuery();
                reader.Close();
                connection.Close();
                return Request.CreateResponse(HttpStatusCode.OK, "Request completed");
            }

            reader.Close();
            connection.Close();
           
            return Request.CreateResponse(HttpStatusCode.BadRequest,"There is no element with id "+id);
        }

       
    }

   
}
