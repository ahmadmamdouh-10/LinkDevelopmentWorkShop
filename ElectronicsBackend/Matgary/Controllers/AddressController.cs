using Matgary.BLL.Infra.User;
using Matgary.DAL;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace Matgary.Controllers
{
    [RoutePrefix("api/address")]
    public class AddressController : ApiController
    {
        private readonly DAL.AppContext _context;
        public AddressController()
        {
            _context = new DAL.AppContext();
        }

        [HttpGet, Route("AddressForUser")]
        public IHttpActionResult GetAddress(long userId, long? storeId)
        {
            var addresses = _context.Address
                .Where(a => a.UserId == userId);

            if (storeId.HasValue && storeId.Value != 0)
            {
                //addresses = addresses.Where(a => a.StoreId == storeId);
            }
            var response = new List<AddressViewModelResponse>();

            foreach (var address in addresses)
            {
                response.Add(new AddressViewModelResponse()
                {
                    Id = address.Id,               
                    Street = address.Street,
                    IsDefault = address.IsDefault
                });
            }

            return Ok(response);
        }

        [HttpPost, Route("SetDefaultAddress")]
        public IHttpActionResult SetDefaultAddress(DefaultAddress request)
        {
            var addressInDb = _context.Address
                .FirstOrDefault(a => a.Id == request.Id);

            if (addressInDb == null)
                return NotFound();

            if (addressInDb.IsDefault == true)
                return BadRequest("Address Already Is Default");

            if (request.IsDefault == true)
            {
                _context.Address
                    .Where(a => a.UserId == addressInDb.UserId && a.IsDefault == true)
                    .ToList().ForEach(a => a.IsDefault = false);

            }

            addressInDb.IsDefault = request.IsDefault;

            _context.Entry(addressInDb).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(new AddressViewModelResponse()
            {
                Id = addressInDb.Id,        
                Street = addressInDb.Street,
                IsDefault = addressInDb.IsDefault
            });
        }

        [HttpPost, Route("NewAddress")]
        public IHttpActionResult Create(CreateAddress request)
        {
            var userInDb = _context.Users
                .FirstOrDefault(u => u.Id == request.UserId);

            if (userInDb == null)
                return BadRequest("No User Exists");

            //code for set is default to false
            if (request.IsDefault == true)
            {
                _context.Address
                    .Where(a => a.UserId == request.UserId && a.IsDefault == true)
                    .ToList().ForEach(a => a.IsDefault = false);
            }

            var address = new Address()
            {
                Street = request.Street,
                UserId = request.UserId,
                IsDefault = request.IsDefault,
                StoreId = request.StoreId
            };

            _context.Address.Add(address);
            _context.SaveChanges();

            var storeInDb = _context.Stores.FirstOrDefault(s => s.Id == request.StoreId);
            return Ok(new AddressViewModelResponse()
            {
                Id = address.Id,      
                Street = address.Street,
                IsDefault = address.IsDefault,
                Store = new CityKeyValueModel()
                { Id = storeInDb.Id, Name = storeInDb.Name, NameAr = storeInDb.Name }
            });
        }
    }
}
