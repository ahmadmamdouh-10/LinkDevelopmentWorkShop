using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Matgary.BLL.Infra.Stores;

namespace Matgary.Controllers
{
    [RoutePrefix("api/Store")]

    public class StoreController : ApiController
    {
        private readonly DAL.AppContext _context;
        public StoreController()
        {
            _context = new DAL.AppContext();
        }

        [HttpGet, Route("Stores")]
        public IHttpActionResult GetStores()
        {
            var stores = _context.Stores.ToList()
                .Select(s=> new StoreDto(){
                    Id = s.Id,
                    Logo = s.Logo == null ? null : ConfigurationManager.AppSettings["Image_Url"]+s.Logo,
                    Currency = s.Currency,
                    Name = s.Name,
                });

            return Ok(stores);
        }

        [HttpGet, Route("RandomStore")]
        public IHttpActionResult GetRandomStore()
        {
            var stores = _context.Stores.ToList()
                .Select(s => new StoreDto() {
                    Id = s.Id,
                    Logo = s.Logo == null ? null : ConfigurationManager.AppSettings["Image_Url"]+ s.Logo,
                    Currency = s.Currency,
                    Name = s.Name,
                })
                .FirstOrDefault();
            return Ok(stores);
        }

        [HttpGet, Route("StoreCurrency")]
        public IHttpActionResult GetStores(long storeId)
        {
            var store = _context.Stores.FirstOrDefault(s => s.Id == storeId);

            if (store == null)
                return BadRequest();

            return Ok(new { Currency = store.Currency });
        }
        
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("GetUserStores")]
        public IHttpActionResult GetUserStores(long userId)
        {
            var user = _context.Users
                .Include(u => u.UserStores)
                .FirstOrDefault(u => u.Id == userId);

            return Ok(user.UserStores.Select(us => new StoreDto(){
                Logo = us.Store.Logo == null ? null : ConfigurationManager.AppSettings["Image_Url"] + us.Store.Logo,
                Currency = us.Store.Currency,
                Name = us.Store.Name,
                Id = us.StoreId
            }));
        }
    }


}
