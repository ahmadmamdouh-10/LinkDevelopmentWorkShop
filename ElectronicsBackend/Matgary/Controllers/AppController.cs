using Matgary.BLL;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Description;
using Matgary.DAL;

namespace Matgary.Controllers
{
    [RoutePrefix("api/app")]
    public class AppController : ApiController
    {
        private readonly DAL.AppContext _db = new DAL.AppContext();

        [HttpGet, Route("aboutus")]
        [ResponseType(typeof(BLL.About))]
        public BLL.About GetAboutUs(long storeId)
        {
            var baseUrl = ConfigurationManager.AppSettings["Image_Url"].ToString();
            var about = _db.Abouts
                .Select(c => new BLL.About
                {
                    Id = c.Id,
                    FacebookLink = c.FacebookLink,
                    Contact = c.Contact,
                    InstagramLink = c.InstagramLink,
                    PintrestLink = c.PintrestLink,
                    TwitterLink = c.TwitterLink,
                    YoutubeLink = c.YoutubeLink,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl,
                    Location = c.Location,
                    Phone = c.Phone,
                    WhatsApp = c.WhatsApp

                }).FirstOrDefault();

            return about;
        }
    }
}
