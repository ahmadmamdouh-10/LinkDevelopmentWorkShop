using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
namespace MatgaryAdmin
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }

    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        private string[] UserRoles { get; set; }

        public AuthorizeUserAttribute(params string[] roless) : base()
        {
            this.Roles = string.Join(",", roless);
            HttpContext.Current.Session["User"] = null;
        }
        // Custom property
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            Matgary.DAL.AppContext db = new Matgary.DAL.AppContext();
            UserRoles = Roles.Split(Convert.ToChar(","));
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null && !authTicket.Expired)
                {
                    var roles = authTicket.UserData.Split(',');
                    HttpContext.Current.User =
                        new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
                    string dd = (string)HttpContext.Current.Session["User"] ?? HttpContext.Current.User.Identity.Name;
                    if (dd == null)
                        return false;
                    bool res = false;
                    foreach (var rol in UserRoles)
                    {
                        if (HttpContext.Current.User.IsInRole(rol.ToString()))
                        {
                            var adminInDb = db.Admins.FirstOrDefault(x => x.UserName == dd);
                            HttpContext.Current.Session["User"] = adminInDb.UserName;
                            HttpContext.Current.Session["StoreId"] = adminInDb.StoreId;
                            HttpContext.Current.Session["Role"] =
                                rol.ToLower() == "admin" ? "Admin" :
                                rol.ToLower() == "superadmin" ? "SuperAdmin" : null;
                            res = true;
                            break;
                        }
                    }
                    return res;
                }
                else
                {
                    HttpContext.Current.Session["User"] = null;

                    return false;
                }
            }
            else
            {
                HttpContext.Current.Session["User"] = null;
                return false;
            }

        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "User",
                                action = "Login"
                            })
                        );
        }


    }
}
