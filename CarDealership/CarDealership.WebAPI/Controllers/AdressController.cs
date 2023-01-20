using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarDealership.Model;
using CarDealership.Service;
using CarDealership.WebAPI.Models;

namespace CarDealership.WebAPI.Controllers
{
    public class AdressController : ApiController
    {
        public static AdressService adressService = new AdressService();


        // GET: api/Adress
        public HttpResponseMessage Get()
        {
            List<Adress> adresses = adressService.Get();
            if (adresses.Any())
            {
                List<AdressRest> adressesRest = new List<AdressRest>();
                adresses.ForEach(adress => adressesRest.Add(AdressRest.MapToAdressRest(adress)));
                return Request.CreateResponse(HttpStatusCode.OK, adressesRest);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "There are no adresses saved");
        }

        // GET: api/Adress/5
        public HttpResponseMessage Get(Guid id)
        {
            Adress adress = adressService.Get(id);
            if (adress!=null)
            {

                return Request.CreateResponse(HttpStatusCode.OK, AdressRest.MapToAdressRest(adress));
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "There is no adress with id " + id);
        }

        // POST: api/Adress
        public HttpResponseMessage Post([FromBody]AdressRest adress)
        {
            var response = adressService.Post(adress.MapToAdress());
            if (!response.Data)
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, response.Message);
            }
            return Request.CreateResponse(HttpStatusCode.Created, response.Message);
        }

        // PUT: api/Adress/5
        [HttpPut]
        public HttpResponseMessage ChangeAdress(Guid id, [FromBody]AdressRest adress)
        {
            var response = adressService.ChangeAdress(id, adress.MapToAdress());
            if (response.Data)
            {
                return Request.CreateResponse(HttpStatusCode.OK, response.Message);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, response.Message);
        }

        // DELETE: api/Adress/5
        public HttpResponseMessage Delete(Guid id)
        {
            var response = adressService.Delete(id);
            if (response.Data)
            {
                return Request.CreateResponse(HttpStatusCode.OK, response.Message);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, response.Message);
        }
    }
}
