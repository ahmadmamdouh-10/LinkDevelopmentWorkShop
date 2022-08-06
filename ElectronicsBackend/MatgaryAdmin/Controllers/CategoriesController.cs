using Matgary.DAL;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MatgaryAdmin.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Tls;
using AppContext = Matgary.DAL.AppContext;

namespace MatgaryAdmin.Controllers
{
    [AuthorizeUser(Roles = "Admin,SuperAdmin")]
    public class CategoriesController : Controller
    {
        private AppContext db = new AppContext();

        public ActionResult Index(string store)
        {
            var storeId = Session["StoreId"]?.ToString();

            var categories = db.Categories.AsEnumerable();

            if (!string.IsNullOrEmpty(storeId))
            {
                var currentStoreId = long.Parse(storeId);
                categories = categories.Where(c => c.StoreId == currentStoreId);
            }
            if (!string.IsNullOrEmpty(store))
            {
                var currentStoreId = Convert.ToInt32(store);

                categories = categories.Where(p => p.StoreId == currentStoreId);
            }

            ViewBag.Stores = new SelectList(db.Stores, "Id", "Name");


            return View(categories);
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category, HttpPostedFileBase upload)
        {
            category.StoreId = (long)Session["StoreId"];
            if (upload != null && upload.ContentLength > 0)
            {
                string fileName = Path.Combine(Server.MapPath("~/Content/images"), upload.FileName);
                //var fileName = WebConfigurationManager.AppSettings["imagePath"] + time + upload.FileName;
                upload.SaveAs(fileName);
                category.ImageUrl = upload.FileName;
            }
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(category);
        }


        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {

                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category, HttpPostedFileBase upload)
        {

            var categoryInDb = db.Categories.FirstOrDefault(c => c.Id == category.Id);

            if (upload != null && upload.ContentLength > 0)
            {
                string fileName = Path.Combine(Server.MapPath("~/Content/images"), upload.FileName);
                //var fileName = WebConfigurationManager.AppSettings["imagePath"] + time + upload.FileName;
                upload.SaveAs(fileName);
                category.ImageUrl = upload.FileName;
            }
            else
            {
                category.ImageUrl = categoryInDb == null ? "" : categoryInDb.ImageUrl;
            }
            if (ModelState.IsValid)
            {
                categoryInDb.Number = category.Number;
                categoryInDb.Title = category.Title;
                categoryInDb.ImageUrl = category.ImageUrl;
                categoryInDb.DateTime = DateTime.Now;
                db.Entry(categoryInDb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult Delete(long? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {

                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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
