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
    public class AdressController : ApiController
    {
        public static string connectionString = @"Data Source=st-02\SQLEXPRESS;Initial Catalog=CarDealership;Integrated Security=True";


        // GET: api/Adress
        public HttpResponseMessage Get()
        {
            List<Adress> adresses = new List<Adress>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("select * from Adress", connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
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
            if (adresses.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, adresses);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "There are no adresses saved");
        }

        // GET: api/Adress/5
        public HttpResponseMessage Get(Guid id)
        {
            Adress adress = null;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("select * from Adress where AdressId='" +
                id + "'", connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
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
            if (adress!=null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, adress);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "There is no adress with id " + id);
        }

        // POST: api/Adress
        public HttpResponseMessage Post([FromBody]Adress adress)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "The object is incorrectly defined");

            }
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Insert into Adress (PostNumber, City, Street ) "+
                "Values ("+adress.PostNumber+", '"+adress.City+"', '"+adress.Street+"');", connection);

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

        // PUT: api/Adress/5
        [HttpPut]
        public HttpResponseMessage ChangeAdress(Guid id, [FromBody]Adress adress)
        {
            Adress updatedAdress = null;
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand("Select * from Adress where AdressId='" + id + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
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
                command = new SqlCommand("Update Adress set PostNumber='" + updatedAdress.PostNumber +
                    "', City='"+updatedAdress.City+"', Street='"+updatedAdress.Street+"' where AdressId='" + id + "'", connection);
                reader = command.ExecuteReader();
                reader.Close();
                connection.Close();
                return Request.CreateResponse(HttpStatusCode.OK, "Adress is updated");
            }
            connection.Close();
            return Request.CreateResponse(HttpStatusCode.NotFound, "there is no sent adress in the list");
        }

        // DELETE: api/Adress/5
        public HttpResponseMessage Delete(Guid id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Select * from Adress where AdressId='" + id + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                reader.Close();
                command = new SqlCommand("Delete from Adress where AdressId='" + id + "'", connection);
                try
                {
                    command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, ex.Message);
                    throw;
                }
                
                
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
