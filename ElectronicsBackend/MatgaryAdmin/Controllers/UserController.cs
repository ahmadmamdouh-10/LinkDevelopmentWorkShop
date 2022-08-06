using Matgary.BLL.Infra.User;
using Matgary.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MatgaryAdmin.Controllers
{
    public class UserController : Controller
    {
        private Matgary.DAL.AppContext db = new Matgary.DAL.AppContext();


        [AllowAnonymous]
        public ActionResult Index()
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null && !authTicket.Expired)
                {
                    var roles = authTicket.UserData.Split(',');
                    var User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
                    var userName = User.Identity.Name;
                    var userNam = db.Admins.Where(x => x.UserName == userName).FirstOrDefault();
                    Session["User"] = userNam.UserName;
                }
            }
            return View();
        }

        [AuthorizeUser(Roles = "Admin,SuperAdmin")]
        public ActionResult Users()
        {
            var storeId = (long)Session["StoreId"];
            var users = db.UserStores
                .ToList();

            var model = new List<Models.UserViewModel>();
            foreach (var user in users)
            {
                var address = db.Address
                    .FirstOrDefault(a => a.IsDefault == true && a.UserId == user.UserId && a.StoreId == storeId);

                var userModel = new Models.UserViewModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Gender = user.Gender,
                    Email = user.User.Email,
                    CreatedAt = user.DateTime,
                    Phone = user.Phone1
                };
                if (address != null)
                {                
                    userModel.Location = address.Street;                       
                }
                model.Add(userModel);


            }
            return View(model);
        }
        // GET: User
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Admin model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = db.Admins
                .FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);

            if (user != null)
            {
                Session["User"] = user.UserName;
                Session["StoreId"] = user.StoreId;
                if (user.IsSuperAdmin.HasValue && user.IsSuperAdmin.Value == true)
                {
                    Session["Role"] = "SuperAdmin";
                }
                else
                {
                    Session["Role"] = "Admin";

                }
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                var authTicket =
                    new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, DateTime.Now.AddMinutes(2000), false, user.IsSuperAdmin.HasValue ? "SuperAdmin" : "Admin");
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                return RedirectToAction("Index", "User");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            Session["User"] = null;
            Session["StoreId"] = null;
            Session["Role"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "User");
        }
        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Edit()
        {
            Admin admin = db.Admins.Find(1);
            return View(admin);
        }
        [AuthorizeUser(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Admin admin)
        {
            db.Entry(admin).State = EntityState.Modified;
            db.SaveChanges();
            Session["User"] = null;
            return RedirectToAction("Login", "User");
        }
    }
}