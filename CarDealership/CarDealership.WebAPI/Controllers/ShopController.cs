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
    public class ShopController : ApiController
    {

        public static string connectionString = @"Data Source=st-02\SQLEXPRESS;Initial Catalog=CarDealership;Integrated Security=True";

        // GET: api/Shop
        public HttpResponseMessage Get()
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
            if (shops.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, shops);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "There are no shoops saved");
        }

        // GET: api/Shop/5
        public HttpResponseMessage Get(Guid id)
        {
            Shop shop = null;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("select * from Shop left join Adress on Shop.Adress=Adress.AdressId where ShopId='"+
                id+"'", connection);
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
            if (shop!=null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, shop);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "There is no shop with id "+id);
        }

        // POST: api/Shop
        public HttpResponseMessage Post(Shop shop)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "The object is incorrectly defined");

            }
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Insert into Shop (ShopName ,Adress) values "+
                "('"+shop.ShopName+"','"+shop.ShopAdress.AdressId+"')", connection);

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

        // PUT: api/Shop/5
        [HttpPut]
        public HttpResponseMessage UpdateShopName(Guid id, [FromBody]Shop shop)
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
                command = new SqlCommand("Update Shop set ShopName='"+updatedShop.ShopName+"' where ShopId='"+id+"'", connection);
                reader = command.ExecuteReader();
                reader.Close();
                connection.Close();
                return Request.CreateResponse(HttpStatusCode.OK, "Shop is updated");
            }
            connection.Close();
            return Request.CreateResponse(HttpStatusCode.NotFound, "there is no sent car in the list");
        }

        // DELETE: api/Shop/5
        public HttpResponseMessage Delete(Guid id)
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
                return Request.CreateResponse(HttpStatusCode.OK, "Request completed");
            }

            reader.Close();
            connection.Close();

            return Request.CreateResponse(HttpStatusCode.BadRequest, "There is no element with id " + id);

        }
    }
}
