using System;
using Matgary.DAL;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppContext = Matgary.DAL.AppContext;

namespace MatgaryAdmin.Controllers
{
    [AuthorizeUser("SuperAdmin")]
    public class StoresController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Stores
        public ActionResult Index()
        {
            return View(db.Stores.ToList());
        }

        // GET: Stores/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // GET: Stores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Store store,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var logoPath = string.Empty;
                if (file != null)
                {
                    var directory = Path.Combine(Server.MapPath("~/Content/images"));
                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    var fileName = Path.Combine(directory, file.FileName);
                    file.SaveAs(fileName);

                    logoPath =  file.FileName;
                }
                    //add store
                    store.Logo = logoPath;
                db.Stores.Add(store);

                //create default back and video
                var backgroundAndVideos = db.BackgroundsAndVideos
                    .FirstOrDefault();
                backgroundAndVideos.StoreId = store.Id;
                db.BackgroundsAndVideos.Add(backgroundAndVideos);

                //create default about 
                var about = db.Abouts
                    .FirstOrDefault();
                about.StoreId = store.Id;
                db.Abouts.Add(about);

                //create default general setting
                var gSetting = db.GeneralSettings
                    .FirstOrDefault();
                gSetting.StoreId = store.Id;
                db.GeneralSettings.Add(gSetting);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(store);
        }

        // GET: Stores/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Store store,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var logoPath = store.Logo;
                if (file != null)
                {
                    var directory = Path.Combine(Server.MapPath("~/Content/images"));
                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    var fileName = Path.Combine(directory, file.FileName);
                    file.SaveAs(fileName);

                    logoPath =  file.FileName;
                }

                store.Logo = logoPath;
                db.Entry(store).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(store);
        }

        // GET: Stores/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Store store = db.Stores.Find(id);
            db.Stores.Remove(store);
            db.SaveChanges();
            return RedirectToAction("Index");
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
