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

        // GET: api/Shop
        public List<Shop> Get()
        {
            List<Shop> shops = new List<Shop>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("select * from Shop left join Adress on Shop.Adress=Adress.AdressId", connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
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

        // GET: api/Shop/5
        public Shop Get(Guid id)
        {
            Shop shop = null;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("select * from Shop left join Adress on Shop.Adress=Adress.AdressId where ShopId='" +
                id + "'", connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
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

        
        public bool Post(Shop shop)
        {
            
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Insert into Shop (ShopName ,Adress) values " +
                "('" + shop.ShopName + "','" + shop.ShopAdress.AdressId + "')", connection);

            connection.Open();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
            }
            catch (Exception)
            {
                return false;
                throw;
            }

            connection.Close();
            return true;
        }

        // PUT: api/Shop/5
        
        public bool UpdateShopName(Guid id,  Shop shop)
        {
            Shop updatedShop = null;
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand("Select * from Shop where ShopId='" + id + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                updatedShop = new Shop()
                {
                    ShopName = shop.ShopName ?? null
                };
            }
            reader.Close();
            if (updatedShop.ShopName != null)
            {
                command = new SqlCommand("Update Shop set ShopName='" + updatedShop.ShopName + "' where ShopId='" + id + "'", connection);
                reader = command.ExecuteReader();
                reader.Close();
                connection.Close();
                return true;
            }
            connection.Close();
            return false;
        }

        // DELETE: api/Shop/5
        public bool Delete(Guid id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Select * from Shop where ShopId='" + id + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                reader.Close();
                command = new SqlCommand("Delete from Shop where ShopId='" + id + "'", connection);
                command.ExecuteNonQuery();
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
