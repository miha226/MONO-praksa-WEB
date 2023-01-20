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


        // GET: api/Adress
        public List<Adress> Get()
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


            return adresses;
        }

        // GET: api/Adress/5
        public Adress Get(Guid id)
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


            return adress;
        }

        // POST: api/Adress
        public ResponseWrapper<bool> Post( Adress adress)
        {
            if(adress.Street == null || adress.City == null || adress.Street==null || adress.PostNumber==0)
            {
                return new ResponseWrapper<bool> { Data = false, Message = "Fields can't be empty" };
            }
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Insert into Adress (PostNumber, City, Street ) " +
                "Values (" + adress.PostNumber + ", '" + adress.City + "', '" + adress.Street + "');", connection);

            connection.Open();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                connection.Close();
                return new ResponseWrapper<bool> { Data = false, Message = ex.Message };
                throw;
            }

            connection.Close();
            return new ResponseWrapper<bool> { Data = true, Message = "Adress is added in database" };
        }

        // PUT: api/Adress/5
        
        public ResponseWrapper<bool> ChangeAdress(Guid id,  Adress adress)
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
                    "', City='" + updatedAdress.City + "', Street='" + updatedAdress.Street + "' where AdressId='" + id + "'", connection);
                reader = command.ExecuteReader();
                reader.Close();
                connection.Close();
                return new ResponseWrapper<bool> { Data = true, Message = "Adress is updated" };
            }
            connection.Close();
            return new ResponseWrapper<bool> { Data = false, Message = "There is no sent adress in the database" };
        }

        // DELETE: api/Adress/5
        public ResponseWrapper<bool> Delete(Guid id)
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
