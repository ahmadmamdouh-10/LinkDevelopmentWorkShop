using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace Matgary.Controllers
{
    [RoutePrefix("api/Settings")]
    public class SettingsController : ApiController
    {
        private readonly DAL.AppContext _db = new DAL.AppContext();
        private readonly string baseUrl = ConfigurationManager.AppSettings["Image_Url"].ToString();

        [HttpGet, Route("AppBackgrounds")]
        public IHttpActionResult GetAppBackgrounds(long? storeId)
        {
            var backgroundsAndVideos = _db.BackgroundsAndVideos.FirstOrDefault();
            if (storeId.HasValue && storeId.Value != 0)
            {
                backgroundsAndVideos = _db.BackgroundsAndVideos.FirstOrDefault(b => b.StoreId == storeId);
            }

            return Ok(new
            {
                WelcomePageBackgroundImageUrl = backgroundsAndVideos.WelcomePageBackgroundImageUrl == null ? "" : baseUrl + backgroundsAndVideos.WelcomePageBackgroundImageUrl,
                LogInPageBackgroundImageUrl = backgroundsAndVideos.LogInPageBackgroundImageUrl == null ? "" : baseUrl + backgroundsAndVideos.LogInPageBackgroundImageUrl,
                RegistrationPageBackgroundImageUrl = backgroundsAndVideos.RegistrationPageBackgroundImageUrl == null ? "" : baseUrl + backgroundsAndVideos.RegistrationPageBackgroundImageUrl,
                HomePageBackgroundImageUrl = backgroundsAndVideos.HomePageBackgroundImageUrl == null ? "" : baseUrl + backgroundsAndVideos.HomePageBackgroundImageUrl,
            });
        }

        [HttpGet, Route("AppLogInPageBackground")]
        public IHttpActionResult GetAppLogInPageBackground(long? storeId)
        {
            var backgroundsAndVideos = _db.BackgroundsAndVideos.FirstOrDefault();
            if (storeId.HasValue && storeId.Value != 0)
                backgroundsAndVideos = _db.BackgroundsAndVideos.FirstOrDefault(b => b.StoreId == storeId);

            return Ok(new
            {
                LogInPageBackgroundImageUrl = backgroundsAndVideos.LogInPageBackgroundImageUrl == null ? "" : baseUrl + backgroundsAndVideos.LogInPageBackgroundImageUrl,
            });
        }

        [HttpGet, Route("AppRegistrationPageBackground")]
        public IHttpActionResult GetAppRegistrationPageBackground(long? storeId)
        {
            var backgroundsAndVideos = _db.BackgroundsAndVideos.FirstOrDefault();
            if (storeId.HasValue && storeId.Value != 0)
                backgroundsAndVideos = _db.BackgroundsAndVideos.FirstOrDefault(b => b.StoreId == storeId);

            return Ok(new
            {
                RegistrationPageBackgroundImageUrl = backgroundsAndVideos.RegistrationPageBackgroundImageUrl == null ? "" : baseUrl + backgroundsAndVideos.RegistrationPageBackgroundImageUrl,
            });
        }

        [HttpGet, Route("AppWelcomePageBackground")]
        public IHttpActionResult GetAppWelcomePageBackground(long? storeId)
        {
            var backgroundsAndVideos = _db.BackgroundsAndVideos.FirstOrDefault();
            if (storeId.HasValue && storeId.Value != 0)
                backgroundsAndVideos = _db.BackgroundsAndVideos.FirstOrDefault(b => b.StoreId == storeId);

            return Ok(new
            {
                WelcomePageBackgroundImageUrl = backgroundsAndVideos.WelcomePageBackgroundImageUrl == null ? "" : baseUrl + backgroundsAndVideos.WelcomePageBackgroundImageUrl,
            });
        }

        [HttpGet, Route("AppHomePageBackground")]
        public IHttpActionResult GetAppHomePageBackground(long? storeId)
        {
            var backgroundsAndVideos = _db.BackgroundsAndVideos.FirstOrDefault();
            if (storeId.HasValue && storeId.Value != 0)
                backgroundsAndVideos = _db.BackgroundsAndVideos.FirstOrDefault(b => b.StoreId == storeId);

            return Ok(new
            {
                HomePageBackgroundImageUrl = backgroundsAndVideos.HomePageBackgroundImageUrl == null ? "" : baseUrl + backgroundsAndVideos.HomePageBackgroundImageUrl,
            });
        }

        [HttpGet, Route("AppHomeVideo")]
        public IHttpActionResult GetAppHomeVideo(long? storeId)
        {
            var backgroundsAndVideos = _db.BackgroundsAndVideos.FirstOrDefault();
            if (storeId.HasValue && storeId.Value != 0)
                backgroundsAndVideos = _db.BackgroundsAndVideos.FirstOrDefault(b => b.StoreId == storeId);

            return Ok(new
            {
                HomePageBackgroundVideoUrl = backgroundsAndVideos.HomePageBackgroundVideoUrl == null ? "" : baseUrl + backgroundsAndVideos.HomePageBackgroundVideoUrl,
            });
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