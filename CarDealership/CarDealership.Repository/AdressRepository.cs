using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Model;
using CarDealership.Repository.Common;

namespace CarDealership.Repository
{
    public class AdressRepository : IAdressRepository
    {
        public static string connectionString = @"Data Source=st-02\SQLEXPRESS;Initial Catalog=CarDealership;Integrated Security=True";


        
        public async Task<List<Adress>> Get()
        {
            List<Adress> adresses = new List<Adress>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("select * from Adress", connection);
            await connection.OpenAsync();

            SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                adresses.Add(new Adress()
                {
                    AdressId = (Guid)reader[0],
                    PostNumber = (int)reader[1],
                    City = (string)reader[2],
                    Street = (string)reader[3]
                });

            }
            reader.Close();
            connection.Close();


            return adresses;
        }

        
        public async Task<Adress> Get(Guid id)
        {
            Adress adress = null;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("select * from Adress where AdressId='" +
                id + "'", connection);
            await connection.OpenAsync();

            SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                adress = new Adress()
                {
                    AdressId = (Guid)reader[0],
                    PostNumber = (int)reader[1],
                    City = (string)reader[2],
                    Street = (string)reader[3]
                };

            }
            reader.Close();
            connection.Close();


            return adress;
        }

        
        public async Task<ResponseWrapper<bool>> Post( Adress adress)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Insert into Adress (AdressId,PostNumber, City, Street ) " +
                "Values (@id,@postNumber, @citiy, @street);", connection);
            command.Parameters.AddWithValue("@id", adress.AdressId);
            command.Parameters.AddWithValue("@postNumber", adress.PostNumber);
            command.Parameters.AddWithValue("@citiy", adress.City);
            command.Parameters.AddWithValue("@street", adress.Street);

            await connection.OpenAsync();
            try
            {
                SqlDataReader reader = await command.ExecuteReaderAsync();
                connection.Close();
                return new ResponseWrapper<bool> { Data = true, Message = "Adress is added in database" };
            }
            catch (Exception ex)
            {
                connection.Close();
                return new ResponseWrapper<bool> { Data = false, Message = ex.Message };
                throw;
            }
        }

        
        
        public async Task<ResponseWrapper<bool>> ChangeAdress(Guid id,  Adress adress)
        {
            Adress updatedAdress = null;
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand("Select * from Adress where AdressId=@id", connection);
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                updatedAdress = new Adress()
                {
                    PostNumber = adress.PostNumber == 0 ? (int)reader[1] : adress.PostNumber,
                    City = adress.City ?? (string)reader[2],
                    Street = adress.Street ?? (string)reader[3]
                };
            }
            reader.Close();
            if (updatedAdress != null)
            {
                command = new SqlCommand("Update Adress set PostNumber=@postNumber"  +
                    ", City=@city, Street=@street where AdressId=@id", connection);
                command.Parameters.AddWithValue("@postNumber", updatedAdress.PostNumber);
                command.Parameters.AddWithValue("@street", updatedAdress.Street);
                command.Parameters.AddWithValue("@city", updatedAdress.City);
                command.Parameters.AddWithValue("@id", id);
                reader = await command.ExecuteReaderAsync();
                reader.Close();
                connection.Close();
                return new ResponseWrapper<bool> { Data = true, Message = "Adress is updated" };
            }
            connection.Close();
            return new ResponseWrapper<bool> { Data = false, Message = "There is no sent adress in the database" };
        }

       
        public async Task<ResponseWrapper<bool>> Delete(Guid id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Select * from Adress where AdressId=@id", connection);
            command.Parameters.AddWithValue("@id", id);
            await connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                reader.Close();
                command = new SqlCommand("Delete from Adress where AdressId=@id", connection);
                command.Parameters.AddWithValue("@id", id);
                try
                {
                    await command.ExecuteNonQueryAsync();

                }
                catch (Exception ex)
                {
                    reader.Close();
                    connection.Close();
                    return new ResponseWrapper<bool> { Data = false, Message=ex.Message };
                    throw;
                }


                reader.Close();
                connection.Close();
                return new ResponseWrapper<bool> { Data = true, Message = "Element with id "+id+" is deleted" };
            }

            reader.Close();
            connection.Close();

            return new ResponseWrapper<bool> {Data=false, Message="There is no element with id "+id };
        }

    }
}
