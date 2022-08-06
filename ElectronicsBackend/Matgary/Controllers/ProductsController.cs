using Matgary.BLL;
using Matgary.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Matgary.Controllers
{
    [RoutePrefix("api/product")]
    public class ProductsController : ApiController
    {
        private readonly DAL.AppContext _db = new DAL.AppContext();


        [HttpGet, Route("LatestProducts")]
        public List<LatestProductSampleModel> GetLastestProducts(long? storeId)
        {
            var products = _db.Products
                .Include(p => p.ProductCategories)
                .OrderByDescending(p => p.DateTime)
                .Where(p => p.InStock == true && p.StoreId == storeId) //new
                .Take(10)
                .ToList();

            var final = products.Select(p => new LatestProductSampleModel()
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                ImageUrl = p.ImageUrl == null ? p.ImageUrl : ConfigurationManager.AppSettings["Image_Url"] + p.ImageUrl,
                InStock = p.InStock,
                Discount = p.Discount,
                Category=string.Join(" - ", p.ProductCategories.Select(pc => pc.Category.Title))
                
            }).ToList();

            return final;
        }

        // GET: api/Products/5
        [ResponseType(typeof(ProductModel)), Route("GetById")]
        public IHttpActionResult GetProduct(long productId, long? userId = -1)
        {
            var product = _db.Products
                .Include(p => p.Images)
                .Include(p => p.ProductCategories)
                .FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return NotFound();
            }

            var prod = new ProductModel
            {
                Category = string.Join(" - ", product.ProductCategories.Select(pc => pc.Category.Title)),
                Discount = product.Discount,
                Title = product.Title,
                Description = product.Description,
                Images = product.Images.Select(i => ConfigurationManager.AppSettings["Image_Url"] + i.ImageUrl).ToList(),
                Id = product.Id,
                InStock = product.InStock
            };      
            return Ok(prod);
        }

        [ResponseType(typeof(List<ProductModel>)), Route("GetByCategory")]
        public IHttpActionResult GetProductsByCategory(long? storeId, string category)
        {
            var products = _db.Products
                .Include(p => p.Images)
                .Include(p => p.ProductCategories)
                .Where(p => p.InStock == true &&
                            (p.ProductCategories.Select(c=>c.Category.Title).Contains(category)))
                .ToList();

            if (storeId.HasValue && storeId.Value != 0)
            {
                products = products.Where(p => p.StoreId == storeId).ToList();
            }
            var response = new List<ProductModel>();
            foreach (var product in products)
            {
                var prod = new ProductModel
                {
                    //Category = lang == "ar" ? product.Category.TitleA : product.Category.TitleE,
                    Category = string.Join(" - ", product.ProductCategories.Select(c => c.Category.Title)),
                    Discount = product.Discount,
                    Title = product.Title,
                    Description = product.Description,
                    Images = product.Images.Select(i => ConfigurationManager.AppSettings["Image_Url"] + i.ImageUrl).ToList(),
                    Id = product.Id,
                    InStock = product.InStock,
                    Price = product.Price,
                    Rate = product.Rate
                };
                response.Add(prod);
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