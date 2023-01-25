using CarDealership.Common;
using CarDealership.Model;
using CarDealership.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Repository
{
    public class CarRepository : ICarRepository
    {
        public static string connectionString = @"Data Source=st-02\SQLEXPRESS;Initial Catalog=CarDealership;Integrated Security=True";


        public async Task<List<Car>> GetAsync(Sorting sorting, Paging paging, FilterCar filter)
        {
            List<Car> cars = new List<Car>();
            SqlConnection connection = new SqlConnection(connectionString);
            StringBuilder stringBuilder = new StringBuilder("Select * from Car");
            if (sorting.CheckIfPropertyNameExists(new Car()) && sorting.OrderDirectionIsValid())
            {
                if (filter.IsNotEmpty())
                {
                    stringBuilder.Append(" where");
                    foreach (var property in filter.GetType().GetProperties())
                    {
                        if (property.GetValue(filter) != null)
                        {
                            string[] words = stringBuilder.ToString().Split(' ');
                            if (words[words.Length - 1] != "where")
                            {
                                stringBuilder.Append(" AND ");
                            }
                            if (property.PropertyType != typeof(DateTime?))
                            {
                                if (property.Name == "KilometersTraveled")
                                {
                                    stringBuilder.AppendFormat(" {0} < '{1}'", property.Name, property.GetValue(filter));
                                }
                                else if (property.Name == "TopSpeed")
                                {
                                    stringBuilder.AppendFormat(" {0} > '{1}'", property.Name, property.GetValue(filter));
                                }
                                else
                                {
                                    stringBuilder.AppendFormat(" {0} = '{1}'", property.Name, property.GetValue(filter));
                                }
                            }

                        }
                    }
                    if (filter.DateNotEmpty())
                    {
                        string[] words = stringBuilder.ToString().Split(' ');
                        if (words[words.Length - 1] != "where")
                        {
                            stringBuilder.Append(" AND ");
                        }
                        stringBuilder.Append(" YearOfManufacture between @minDate AND @maxDate ");
                    }
                }


                stringBuilder.AppendFormat(" order by {0} {1} offset {2} rows fetch next {3} rows only",
                    sorting.OrderName, sorting.OrderDirection, paging.PageNumber * paging.PageSize, paging.PageSize);

                SqlCommand command = new SqlCommand(stringBuilder.ToString(), connection);
                if (filter.DateNotEmpty())
                {
                    command.Parameters.AddWithValue("@minDate", filter.DateMin);
                    command.Parameters.AddWithValue("@maxDate", filter.DateMax);
                }
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
            }
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


        /*public async Task<ResponseWrapper<bool>> Post(List<Car> cars)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand command;
            try
            {
                var tasks = new List<Task>();
                foreach (var car in cars)
                {
                    command = new SqlCommand("Insert into Car (CarId,ManufacturerName,Model,YearOfManufacture,KilometersTraveled,TopSpeed,Color,StoredInShop) " +
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
                    tasks.Add(command.ExecuteNonQueryAsync());
                }
                Task.WaitAll(tasks.ToArray());
                transaction.Commit();
                transaction.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                return new ResponseWrapper<bool> { Data = false, Message = ex.Message };                
                throw;
            }
            
            return new ResponseWrapper<bool> { Data = true, Message = "Car is added in database" };
        }*/





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
