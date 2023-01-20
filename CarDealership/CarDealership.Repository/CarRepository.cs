using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Repository.Common;
using CarDealership.Model;
using System.Data.SqlClient;

namespace CarDealership.Repository
{
    public class CarRepository : ICarRepository
    {
        public static string connectionString = @"Data Source=st-02\SQLEXPRESS;Initial Catalog=CarDealership;Integrated Security=True";

        // GET: api/Car
        public List<Car> Get()
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
            return cars;
        }

        // GET: api/Car/5
        public Car Get(Guid id)
        {
            Car car = null;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Select * from Car where CarId='" + id + "'", connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                 car = new Car()
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
                
            }
            connection.Close();
            return car;

        }

        // POST: api/Car
        public ResponseWrapper<bool> Post(Car car)
        {
            if(car.Color == null || car.KilometersTraveled == 0 || car.ManufacturerName == null 
                || car.Model == null || car.StoredInShop == null || car.TopSpeed == 0 || car.Year == 0 )
            {
                return new ResponseWrapper<bool> { Data = false, Message = "Fields can't be empty" };
            }
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Insert into Car (ManufacturerName,Model,YearOfManufacture,KilometersTraveled,TopSpeed,Color,StoredInShop) " +
            "values('" + car.ManufacturerName + "', '" + car.Model + "', " + car.Year + ", " + car.KilometersTraveled + ", " + car.TopSpeed +
            ", '" + car.Color + "', '" + car.StoredInShop + "')", connection);

            connection.Open();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<bool> { Data = false, Message = ex.Message };
                throw;
            }

            connection.Close();
            return new ResponseWrapper<bool> { Data = true, Message = "Car is added in database" };
        }





        public ResponseWrapper<bool> Put(Guid id, Car car)
        {
            Car updatedCar = null;
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
                    TopSpeed = car.TopSpeed == 0 ? int.Parse(reader["TopSpeed"].ToString()) : car.TopSpeed
                };
            }
            reader.Close();
            if (updatedCar != null)
            {
                command = new SqlCommand("Update car set Color='" + updatedCar.Color + "', YearOfManufacture=" + updatedCar.Year +
                    ", ManufacturerName='" + updatedCar.ManufacturerName +
                    "', Model='" + updatedCar.Model +
                    "', TopSpeed=" + updatedCar.TopSpeed +
                    ", KilometersTraveled=" + updatedCar.KilometersTraveled + " where CarId='"+id+"'", connection);
                reader = command.ExecuteReader();
                reader.Close();
                connection.Close();
                return new ResponseWrapper<bool> { Data = true, Message = "Car is updated" };
            }
            connection.Close();
            return new ResponseWrapper<bool> { Data = false, Message = "car does not exist" };
        }


        // DELETE: api/Car/5
        public ResponseWrapper<bool> Delete( Guid id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Select * from Car where CarId='" + id + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                reader.Close();
                command = new SqlCommand("Delete from Car where CarId='" + id + "'", connection);
                command.ExecuteNonQuery();
                reader.Close();
                connection.Close();
                return new ResponseWrapper<bool> { Data = true, Message = "Car with id " + id + " is deleted" };
            }

            reader.Close();
            connection.Close();

            return new ResponseWrapper<bool> { Data = false, Message = "There is no element with id " + id };
        }


    }
}
