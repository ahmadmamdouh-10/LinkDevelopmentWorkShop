using Matgary.DAL;
using MatgaryAdmin.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MatgaryAdmin.Controllers
{
    [AuthorizeUser(Roles = "Admin,SuperAdmin")]
    public class SettingsController : Controller
    {
        private Matgary.DAL.AppContext db = new Matgary.DAL.AppContext();

        public ActionResult BackgroundsAndVideos()
        {
            var storeId = Session["StoreId"]?.ToString();
            var backgroundAndVideo = new BackgroundsAndVideos();
            if (!string.IsNullOrEmpty(storeId))
            {
                var currentStoreId = int.Parse(storeId);
                backgroundAndVideo = db.BackgroundsAndVideos
                    .FirstOrDefault(p => p.StoreId == currentStoreId);
            }
            return View(backgroundAndVideo);
        }
        public ActionResult BackgroundsAndVideosForSuberAdmin(string store)
        {
            ViewBag.Stores = new SelectList(db.Stores, "Id", "Name");

            var backgroundAndVideos = db.BackgroundsAndVideos
                .ToList();

            if (!string.IsNullOrEmpty(store))
            {
                var currentStoreId = int.Parse(store);
                backgroundAndVideos = backgroundAndVideos.Where(b => b.StoreId == currentStoreId).ToList();
            }
            return View(backgroundAndVideos);
        }

        public ActionResult WelcomePageBackgroundImage(int id)
        {
            BackgroundsAndVideos backgroundsAndVideos = db.BackgroundsAndVideos.Find(id);
            ImageVideoUploadViewModel imageVideoUploadViewModel = new ImageVideoUploadViewModel();
            imageVideoUploadViewModel.ImageUrl = backgroundsAndVideos.WelcomePageBackgroundImageUrl;
            return View(imageVideoUploadViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> WelcomePageBackgroundImage(ImageVideoUploadViewModel imageVideoUploadViewModel, HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                string fileName = Path.Combine(Server.MapPath("~/Content/images"), upload.FileName);
                upload.SaveAs(fileName);

                BackgroundsAndVideos backgroundsAndVideos = db.BackgroundsAndVideos
                    .Find(imageVideoUploadViewModel.Id);
                backgroundsAndVideos.WelcomePageBackgroundImageUrl = upload.FileName;

                db.Entry(backgroundsAndVideos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("BackgroundsAndVideos");
            }
            return View(imageVideoUploadViewModel);
        }

        public ActionResult LogInPageBackgroundImage(int id)
        {
            BackgroundsAndVideos backgroundsAndVideos = db.BackgroundsAndVideos.Find(id);
            ImageVideoUploadViewModel imageVideoUploadViewModel = new ImageVideoUploadViewModel();
            imageVideoUploadViewModel.ImageUrl = backgroundsAndVideos.LogInPageBackgroundImageUrl;
            return View(imageVideoUploadViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogInPageBackgroundImage(ImageVideoUploadViewModel imageVideoUploadViewModel, HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                string fileName = Path.Combine(Server.MapPath("~/Content/images"), upload.FileName);
                upload.SaveAs(fileName);

                BackgroundsAndVideos backgroundsAndVideos = db.BackgroundsAndVideos
                    .Find(imageVideoUploadViewModel.Id);
                backgroundsAndVideos.LogInPageBackgroundImageUrl = upload.FileName;

                db.Entry(backgroundsAndVideos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("BackgroundsAndVideos");
            }
            return View(imageVideoUploadViewModel);
        }

        public ActionResult RegistrationPageBackgroundImage(int id)
        {
            BackgroundsAndVideos backgroundsAndVideos = db.BackgroundsAndVideos.Find(id);
            ImageVideoUploadViewModel imageVideoUploadViewModel = new ImageVideoUploadViewModel();
            imageVideoUploadViewModel.ImageUrl = backgroundsAndVideos.RegistrationPageBackgroundImageUrl;
            return View(imageVideoUploadViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistrationPageBackgroundImage(ImageVideoUploadViewModel imageVideoUploadViewModel, HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                string fileName = Path.Combine(Server.MapPath("~/Content/images"), upload.FileName);
                upload.SaveAs(fileName);

                BackgroundsAndVideos backgroundsAndVideos = db.BackgroundsAndVideos
                    .Find(imageVideoUploadViewModel.Id);
                backgroundsAndVideos.RegistrationPageBackgroundImageUrl = upload.FileName;

                db.Entry(backgroundsAndVideos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("BackgroundsAndVideos");
            }
            return View(imageVideoUploadViewModel);
        }

        public ActionResult HomePageBackgroundImage(int id)
        {
            BackgroundsAndVideos backgroundsAndVideos = db.BackgroundsAndVideos.Find(id);
            ImageVideoUploadViewModel imageVideoUploadViewModel = new ImageVideoUploadViewModel();
            imageVideoUploadViewModel.ImageUrl = backgroundsAndVideos.HomePageBackgroundImageUrl;
            return View(imageVideoUploadViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> HomePageBackgroundImage(ImageVideoUploadViewModel imageVideoUploadViewModel, HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                string fileName = Path.Combine(Server.MapPath("~/Content/images"), upload.FileName);
                upload.SaveAs(fileName);

                BackgroundsAndVideos backgroundsAndVideos = db.BackgroundsAndVideos.Find(imageVideoUploadViewModel.Id);
                backgroundsAndVideos.HomePageBackgroundImageUrl = upload.FileName;

                db.Entry(backgroundsAndVideos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("BackgroundsAndVideos");
            }
            return View(imageVideoUploadViewModel);
        }

        public ActionResult HomePageBackgroundVideo(int id)
        {
            BackgroundsAndVideos backgroundsAndVideos = db.BackgroundsAndVideos.Find(id);
            ImageVideoUploadViewModel imageVideoUploadViewModel = new ImageVideoUploadViewModel();
            imageVideoUploadViewModel.ImageUrl = backgroundsAndVideos.HomePageBackgroundVideoUrl;
            return View(imageVideoUploadViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> HomePageBackgroundVideo(ImageVideoUploadViewModel imageVideoUploadViewModel, HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                string fileName = Path.Combine(Server.MapPath("~/Content/images"), upload.FileName);
                upload.SaveAs(fileName);

                BackgroundsAndVideos backgroundsAndVideos = db.BackgroundsAndVideos.Find(imageVideoUploadViewModel.Id);
                backgroundsAndVideos.HomePageBackgroundVideoUrl = upload.FileName;

                db.Entry(backgroundsAndVideos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("BackgroundsAndVideos");
            }
            return View(imageVideoUploadViewModel);
        }

        // GET: Abouts
        public async Task<ActionResult> Index()
        {
            return View(await db.Abouts.ToListAsync());
        }

        // GET: Abouts/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = await db.Abouts.FindAsync(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        // GET: Abouts/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = await db.Abouts.FindAsync(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        // POST: Abouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Contact,Location,Phone,WhatsApp,FacebookLink,InstagramLink,PintrestLink,TwitterLink,YoutubeLink,Description,DateTime,ImageUrl")] About about, HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                Random random = new Random();
                string fileName = random.Next(100) + upload.FileName;
                var time = DateTimeOffset.Now.ToUnixTimeSeconds();
                string filePath = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                upload.SaveAs(filePath);
                about.ImageUrl = fileName;
            }
            if (ModelState.IsValid)
            {
                about.DateTime = DateTime.Now;
                db.Entry(about).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(about);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
