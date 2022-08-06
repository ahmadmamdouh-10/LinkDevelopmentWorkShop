using Matgary.BLL.Infra.User;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Matgary.Controllers
{
    [RoutePrefix("api/category")]
    public class CategoriesController : ApiController
    {
        private readonly DAL.AppContext _db = new DAL.AppContext();

        // GET: api/Categories
        [ResponseType(typeof(List<CategoryDto>))]
        [HttpGet, Route("")]
        public List<CategoryDto> GetCategories(long? storeId, string lang = "ar")
        {
            var baseUrl = ConfigurationManager.AppSettings["Image_Url"].ToString();

            var categories = _db.Categories
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    ImageUrl = c.ImageUrl == null ? c.ImageUrl : baseUrl + c.ImageUrl,
                    Title = c.Title,
                    Number = c.Number,
                    StoreId = c.StoreId
                })
                .OrderBy(c=>c.Number)
                .ToList();

        
            if (storeId.HasValue && storeId.Value != 0)
            {
                categories = categories.Where(c => c.StoreId == storeId).ToList();
            }

            return categories;
        }
    }

    public class CategoryDto
    {
        public long Id { get; set; }
        public long StoreId { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string TitleE { get; set; }
        public int Number { get; set; }
        public CityKeyValueModel Store { get; set; }
    }
}