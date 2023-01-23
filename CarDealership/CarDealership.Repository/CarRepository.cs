using CarDealership.Model;
using CarDealership.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CarDealership.Repository
{
    public class CarRepository : ICarRepository
    {
        public static string connectionString = @"Data Source=st-02\SQLEXPRESS;Initial Catalog=CarDealership;Integrated Security=True";

        
        public async Task<List<Car>> GetAsync()
        {
            List<Car> cars = new List<Car>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Select * from Car", connection);
            await connection.OpenAsync();

            SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                cars.Add(new Car()
                {
                    Color = reader["Color"].ToString(),
                    Id = Guid.Parse(reader["CarId"].ToString()),
                    KilometersTraveled = int.Parse(reader["KilometersTraveled"].ToString()),
                    Year = DateTime.Parse(reader["YearOfManufacture"].ToString()),
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

        
        public async Task<Car> Get(Guid id)
        {
            Car car = null;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Select * from Car where CarId=@id", connection);
            command.Parameters.AddWithValue("@id", id);
            await connection.OpenAsync();

            SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                car = new Car()
                {
                    Color = reader["Color"].ToString(),
                    Id = Guid.Parse(reader["CarId"].ToString()),
                    KilometersTraveled = int.Parse(reader["KilometersTraveled"].ToString()),
                    Year = DateTime.Parse(reader["YearOfManufacture"].ToString()),
                    ManufacturerName = reader["ManufacturerName"].ToString(),
                    Model = reader["Model"].ToString(),
                    TopSpeed = int.Parse(reader["TopSpeed"].ToString()),
                    StoredInShop = Guid.Parse(reader["StoredInShop"].ToString())
                };

            }
            reader.Close();
            connection.Close();
            return car;
        }

        
        public async Task<ResponseWrapper<bool>> Post(Car car)
        {
            
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Insert into Car (CarId,ManufacturerName,Model,YearOfManufacture,KilometersTraveled,TopSpeed,Color,StoredInShop) " +
            "values(@id,@manufacturerName, @model, @yearOfManufacture, @kilometersTraveled, @topSpeed" +
            ", @color, @storedInshop)", connection);

            command.Parameters.AddWithValue("@color", car.Color);
            command.Parameters.AddWithValue("@storedInshop", car.StoredInShop);
            command.Parameters.AddWithValue("@yearOfManufacture", car.Year);
            command.Parameters.AddWithValue("@manufacturerName", car.ManufacturerName);
            command.Parameters.AddWithValue("@model", car.Model);
            command.Parameters.AddWithValue("@topSpeed", car.TopSpeed);
            command.Parameters.AddWithValue("@kilometersTraveled", car.KilometersTraveled);
            command.Parameters.AddWithValue("@id", car.Id);

            await connection.OpenAsync();
            try
            {
                SqlDataReader reader = await command.ExecuteReaderAsync();
                reader.Close();
            }
            catch (Exception ex)
            {   
                return new ResponseWrapper<bool> { Data = false, Message = ex.Message };
                throw;
            }

            connection.Close();
            return new ResponseWrapper<bool> { Data = true, Message = "Car is added in database" };
        }





        public async Task<ResponseWrapper<bool>> Put(Guid id, Car car)
        {
            Car updatedCar = null;
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand("Select * from Car where CarId=@id", connection);
            command.Parameters.AddWithValue("@id", id);
            await connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                updatedCar = new Car()
                {
                    Color = car.Color ?? reader["Color"].ToString(),
                    KilometersTraveled = car.KilometersTraveled == 0 ? int.Parse(reader["KilometersTraveled"].ToString()) : car.KilometersTraveled,
                    Year = car.Year == null ? DateTime.Parse(reader["YearOfManufacture"].ToString()) : car.Year,
                    ManufacturerName = car.ManufacturerName ?? reader["ManufacturerName"].ToString(),
                    Model = car.Model ?? reader["Model"].ToString(),
                    TopSpeed = car.TopSpeed == 0 ? int.Parse(reader["TopSpeed"].ToString()) : car.TopSpeed
                };
            }
            reader.Close();
            if (updatedCar != null)
            {
                command = new SqlCommand("Update car set Color=@color, YearOfManufacture=@yearOfManufacture" +
                    ", ManufacturerName=@manufacturerName" +
                    ", Model=@model, TopSpeed=@topSpeed" +
                    ", KilometersTraveled=@kilometersTraveled where CarId=@id", connection);

                command.Parameters.AddWithValue("@color", updatedCar.Color);
                command.Parameters.AddWithValue("@yearOfManufacture", updatedCar.Year);
                command.Parameters.AddWithValue("@manufacturerName", updatedCar.ManufacturerName);
                command.Parameters.AddWithValue("@model", updatedCar.Model);
                command.Parameters.AddWithValue("@topSpeed", updatedCar.TopSpeed);
                command.Parameters.AddWithValue("@kilometersTraveled", updatedCar.KilometersTraveled);
                command.Parameters.AddWithValue("@ID", id);

                reader = await command.ExecuteReaderAsync();
                reader.Close();
                connection.Close();
                return new ResponseWrapper<bool> { Data = true, Message = "Car is updated" };
            }
            connection.Close();
            return new ResponseWrapper<bool> { Data = false, Message = "car does not exist" };
        }


        
        public async Task<ResponseWrapper<bool>> Delete(Guid id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Select * from Car where CarId=@id", connection);
            command.Parameters.AddWithValue("@id", id);
            await connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                reader.Close();
                command = new SqlCommand("Delete from Car where CarId=@id", connection);
                command.Parameters.AddWithValue("@id", id);
                try
                {
                   await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    reader.Close();
                    connection.Close();
                    return new ResponseWrapper<bool> { Data = false, Message = ex.Message };
                    throw;
                }
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
