using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CarDealership.Model;
using CarDealership.Service;
using CarDealership.WebAPI.Models;

namespace CarDealership.WebAPI.Controllers
{
    public class ShopController : ApiController
    {

        public static ShopService shopService = new ShopService();

        // GET: api/Shop
        public async Task<HttpResponseMessage> Get()
        {
            List<Shop> shops = await shopService.Get();
            if (shops.Any())
            {
                List<ShopGetRest> shopsGetRest = new List<ShopGetRest>();
                shops.ForEach(shop => shopsGetRest.Add(ShopGetRest.MapToShopGetRest(shop)));
                return Request.CreateResponse(HttpStatusCode.OK, shopsGetRest);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "There are no shoops saved");
        }

        // GET: api/Shop/5
        public async Task<HttpResponseMessage> Get(Guid id)
        {
            Shop shop = await shopService.Get(id);
            if (shop!=null)
            {
                ShopGetRest shopGetRest = ShopGetRest.MapToShopGetRest(shop);
                return Request.CreateResponse(HttpStatusCode.OK, shopGetRest);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "There is no shop with id "+id);
        }

        // POST: api/Shop
        public async Task<HttpResponseMessage> Post([FromBody]ShopRest shop)
        {
            if (!await shopService.Post(shop.MapToShop()))
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Post failed");
            }
            return Request.CreateResponse(HttpStatusCode.Created, "Object is added in databse");
        }

        // PUT: api/Shop/5
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateShopName(Guid id, [FromBody]ShopRest shop)
        {
            if(await shopService.UpdateShopName(id,shop.MapToShop()))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Shop is updated");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "there is no sent car in the list");
        }

        // DELETE: api/Shop/5
        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            if(await shopService .Delete(id))
            { 
                return Request.CreateResponse(HttpStatusCode.OK, "Delete completed");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "There is no element with id " + id);

        }
    }
}
