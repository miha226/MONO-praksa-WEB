using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public HttpResponseMessage Get()
        {
            List<Shop> shops = shopService.Get();
            if (shops.Any())
            {
                List<ShopRest> shopsRest = new List<ShopRest>();
                shops.ForEach(shop => shopsRest.Add(ShopRest.MapToShopRest(shop)));
                return Request.CreateResponse(HttpStatusCode.OK, shopsRest);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "There are no shoops saved");
        }

        // GET: api/Shop/5
        public HttpResponseMessage Get(Guid id)
        {
            Shop shop = shopService.Get(id);
            if (shop!=null)
            {
                ShopRest shopRest = ShopRest.MapToShopRest(shop);
                return Request.CreateResponse(HttpStatusCode.OK, shopRest);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "There is no shop with id "+id);
        }

        // POST: api/Shop
        public HttpResponseMessage Post(ShopRest shop)
        {
            if (!shopService.Post(shop.MapToShop()))
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Post failed");
            }
            return Request.CreateResponse(HttpStatusCode.Created, "Object is added in databse");
        }

        // PUT: api/Shop/5
        [HttpPut]
        public HttpResponseMessage UpdateShopName(Guid id, [FromBody]ShopRest shop)
        {
            if(shopService.UpdateShopName(id,shop.MapToShop()))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Shop is updated");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "there is no sent car in the list");
        }

        // DELETE: api/Shop/5
        public HttpResponseMessage Delete(Guid id)
        {
            if(shopService.Delete(id))
            { 
                return Request.CreateResponse(HttpStatusCode.OK, "Delete completed");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "There is no element with id " + id);

        }
    }
}
