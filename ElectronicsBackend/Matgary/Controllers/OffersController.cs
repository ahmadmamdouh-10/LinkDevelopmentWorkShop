using Matgary.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Matgary.Controllers
{
    [Route("api/offers")]
    public class OffersController : ApiController
    {
        private readonly DAL.AppContext _db = new DAL.AppContext();

        // GET: api/Offers
        [ResponseType(typeof(List<GetResponse>))]
        public IHttpActionResult GetOffers(long? storeId)
        {
            var baseUrl = ConfigurationManager.AppSettings["Image_Url"].ToString();

            var offers = _db.Offers
                .Where(o => o.ExpirationDateTime >= DateTime.Today)
                .OrderBy(o=>o.Number)
                .ToList();
            
            if (storeId.HasValue && storeId.Value != 0)
            {
                offers = offers.Where(o => o.StoreId == storeId).ToList();
            }

            var response = new List<GetResponse>();
            foreach (var item in offers)
            {
                var res = new GetResponse
                {
                    OfferInformation = new OfferInformation()
                    {
                        Title = item.Title,
                        Description = item.Description,
                        Id = item.Id,
                        Discount = item.Discount,
                        PackPriceAfterDiscount = item.Product.Price - item.Discount,
                        ExpirationDateTime = item.ExpirationDateTime,
                        DurationSeconds = item.DurationSeconds,
                    },
                    ProductDetails = new ProductOffers()
                    {
                        Id = item.ProductId.Value,
                        Title = item.Product.Title,
                        ImagePath = item.Product.ImageUrl == null
                            ? item.Product.ImageUrl
                            : baseUrl + item.Product.ImageUrl,
                    }
                };
                response.Add(res);
            }
            return Ok(response);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}