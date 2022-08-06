using Matgary.BLL.Infra.User;
using MatgaryAdmin.Helpers;
using PagedList;
using System;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using OfficeOpenXml;

namespace MatgaryAdmin.Controllers
{
    [AuthorizeUser(Roles = "Admin,SuperAdmin")]
    public class CustomerController : Controller
    {
        private readonly Matgary.DAL.AppContext _context;

        public CustomerController()
        {
            _context = new Matgary.DAL.AppContext();
        }

        public ActionResult Index(int? page, string store,string phone,string name,int?customerId)
        {
            var storeId = Session["StoreId"]?.ToString();

            var users = _context.UserStores
                .Include(u => u.User)
                .Include(u => u.Store);

            if (!string.IsNullOrEmpty(storeId))
            {
                var currentStoreId = long.Parse(storeId);
                users = users.Where(c => c.StoreId == currentStoreId);
            }

            if (!string.IsNullOrEmpty(store))
            {
                var currentStoreId = Convert.ToInt32(store);

                users = users.Where(us => us.StoreId == currentStoreId);
            }

            if (!string.IsNullOrEmpty(name))
            {
                users = users.Where(us => us.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(phone))
            {
                users = users.Where(us => us.Phone1 == phone);
            }

            if (customerId.HasValue)
            {
                users = users.Where(us => us.User.Id == customerId);
            }
            ViewBag.Stores = new SelectList(_context.Stores, "Id", "Name");

            return View(users.OrderBy(u => u.Id).ToPagedList(page ?? 1, Constants.PageSize));
        }

        public ActionResult Details(int id, int storeId)
        {
            var user = _context.UserStores
                .Include(u => u.User)
                .Include(u => u.Store)
                .FirstOrDefault(u => u.UserId == id && u.StoreId == storeId);

            var address = _context.Address
                .FirstOrDefault(a => a.IsDefault == true && a.UserId == id);

            var userModel = new Models.UserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                Gender = user.Gender,
                Email = user.User.Email,
                CreatedAt = user.DateTime,
                Phone = user.Phone1,
                Phone2 = user.Phone2,
                UserName = user.User.Username
            };
            
            return View(userModel);
        }



        public ActionResult OrderProducts(int id, int storeId)
        {
            var orderProducts = _context.OrderProducts
                .Include(op => op.Product);

            return View(orderProducts);
        }

        [HttpPost]
        public ActionResult Delete(int id, int storeId)
        {
            var userInDb = _context.UserStores
                .FirstOrDefault(u => u.UserId == id && u.StoreId == storeId);

            if (userInDb == null)
                return HttpNotFound("No User Founded");

            var userInUserTable = _context.Users
                .FirstOrDefault(u => u.Id == id);

            if (userInUserTable == null)
                return HttpNotFound("No User Founded");

            _context.UserStores.Remove(userInDb);
            _context.Users.Remove(userInUserTable);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}