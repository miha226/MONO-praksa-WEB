using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CarDealership.Model;
using CarDealership.Service.Common;
using CarDealership.WebAPI.Models;

namespace CarDealership.WebAPI.Controllers
{
    public class AdressController : ApiController
    {
        private IAdressService adressService;

        public AdressController(IAdressService adressService)
        {
            this.adressService = adressService;
        }
        // GET: api/Adress
        public async Task<HttpResponseMessage> Get()
        {
            List<Adress> adresses = await adressService.Get();
            if (adresses.Any())
            {
                List<AdressRest> adressesRest = new List<AdressRest>();
                adresses.ForEach(adress => adressesRest.Add(AdressRest.MapToAdressRest(adress)));
                return Request.CreateResponse(HttpStatusCode.OK, adressesRest);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "There are no adresses saved");
        }

        // GET: api/Adress/5
        public async Task<HttpResponseMessage> Get(Guid id)
        {
            Adress adress = await adressService.Get(id);
            if (adress!=null)
            {

                return Request.CreateResponse(HttpStatusCode.OK, AdressRest.MapToAdressRest(adress));
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "There is no adress with id " + id);
        }

        // POST: api/Adress
        public async Task<HttpResponseMessage> Post([FromBody]AdressPostRest adress)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Adress object is incomplete");

            }
            var response = await adressService.Post(adress.MapToAdress());
            if (!response.Data)
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, response.Message);
            }
            return Request.CreateResponse(HttpStatusCode.Created, response.Message);
        }

        // PUT: api/Adress/5
        [HttpPut]
        public async Task<HttpResponseMessage> ChangeAdress(Guid id, [FromBody]AdressRest adress)
        {
            var response = await adressService.ChangeAdress(id, adress.MapToAdress());
            if (response.Data)
            {
                return Request.CreateResponse(HttpStatusCode.OK, response.Message);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, response.Message);
        }

        // DELETE: api/Adress/5
        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            var response = await adressService.Delete(id);
            if (response.Data)
            {
                return Request.CreateResponse(HttpStatusCode.OK, response.Message);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, response.Message);
        }
    }
}
