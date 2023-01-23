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
    public class ShopRepository : IShopRepository
    {
        public static string connectionString = @"Data Source=st-02\SQLEXPRESS;Initial Catalog=CarDealership;Integrated Security=True";

        
        public async Task<List<Shop>> Get()
        {
            List<Shop> shops = new List<Shop>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("select * from Shop left join Adress on Shop.Adress=Adress.AdressId", connection);
            await connection.OpenAsync();

            SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                shops.Add(new Shop()
                {
                    ShopId = (Guid)reader[0],
                    ShopName = (string)reader[1],
                    ShopAdress = new Adress()
                    {
                        AdressId = (Guid)reader[2],
                        PostNumber = (int)reader[4],
                        City = (string)reader[5],
                        Street = (string)reader[6]
                    }
                });

            }
            reader.Close();
            connection.Close();
            return shops;
        }

        
        public async Task<Shop> Get(Guid id)
        {
            Shop shop = null;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("select * from Shop left join Adress on Shop.Adress=Adress.AdressId where ShopId=@id", connection);
            command.Parameters.AddWithValue("@id", id);
            await connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                shop = new Shop()
                {
                    ShopId = (Guid)reader[0],
                    ShopName = (string)reader[1],
                    ShopAdress = new Adress()
                    {
                        AdressId = (Guid)reader[2],
                        PostNumber = (int)reader[4],
                        City = (string)reader[5],
                        Street = (string)reader[6]
                    }
                };

            }
            reader.Close();
            connection.Close();
            return shop;
        }

        
        public async Task<bool> Post(Shop shop)
        {
            
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Insert into Shop (ShopId,ShopName ,Adress) values " +
                "(@id,@shopname, @shopAdress)", connection);
            command.Parameters.AddWithValue("@id", shop.ShopId);
            command.Parameters.AddWithValue("@shopName", shop.ShopName);
            command.Parameters.AddWithValue("@shopAdress", shop.ShopAdress.AdressId);

            await connection.OpenAsync();
            try
            {
                SqlDataReader reader = await command.ExecuteReaderAsync();
            }
            catch (Exception)
            {
                return false;
                throw;
            }

            connection.Close();
            return true;
        }

        
        
        public async Task<bool> UpdateShopName(Guid id,  Shop shop)
        {
            Shop updatedShop = null;
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand("Select * from Shop where ShopId=@id", connection);
            command.Parameters.AddWithValue("@id", id);
            await connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                updatedShop = new Shop()
                {
                    ShopName = shop.ShopName ?? null
                };
            }
            reader.Close();
            if (updatedShop.ShopName != null)
            {
                command = new SqlCommand("Update Shop set ShopName=@shopName where ShopId=@id", connection);
                command.Parameters.AddWithValue("@shopName", updatedShop.ShopName);
                command.Parameters.AddWithValue("@id", id);
                reader = await command.ExecuteReaderAsync();
                reader.Close();
                connection.Close();
                return true;
            }
            connection.Close();
            return false;
        }

        
        public async Task<bool> Delete(Guid id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Select * from Shop where ShopId=@id", connection);
            command.Parameters.AddWithValue("@id", id);
            await connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                reader.Close();
                command = new SqlCommand("Delete from Shop where ShopId=@id", connection);
                command.Parameters.AddWithValue("@id", id);
                try
                {
                   await command.ExecuteNonQueryAsync();
                }
                catch (Exception)
                {
                    reader.Close();
                    connection.Close();
                    return false;
                    throw;
                }
                reader.Close();
                connection.Close();
                return true;
            }

            reader.Close();
            connection.Close();

            return false;

        }
    }
}
