using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Matgary.DAL;
using AppContext = Matgary.DAL.AppContext;

namespace MatgaryAdmin.Controllers
{
    [AuthorizeUser(Roles = "SuperAdmin,Admin")]
    public class ContactEmailsController : Controller
    {
        private AppContext db = new AppContext();

        // GET: ContactEmails
        public ActionResult Index(string store)
        {
            var storeId = Session["StoreId"]?.ToString();
          
            var contactEmails = db.ContactEmails.AsEnumerable();

            if (!string.IsNullOrEmpty(storeId))
            {
                var currentStoreId = long.Parse(storeId);
                contactEmails = contactEmails.Where(c => c.StoreId == currentStoreId);
            }
            if (!string.IsNullOrEmpty(store))
            {
                var currentStoreId = Convert.ToInt32(store);

                contactEmails= contactEmails.Where(p => p.StoreId == currentStoreId);
            }

            ViewBag.Stores = new SelectList(db.Stores, "Id", "Name");

            return View(contactEmails.ToList());
        }

        // GET: ContactEmails/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactEmail contactEmail = db.ContactEmails.Find(id);
            if (contactEmail == null)
            {
                return HttpNotFound();
            }
            return View(contactEmail);
        }

        // GET: ContactEmails/Create
        public ActionResult Create()
        {
            ViewBag.StoreId = new SelectList(db.Stores, "Id", "Name");
            return View();
        }

        // POST: ContactEmails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,Name,Body,Title,DateTime,StoreId")] ContactEmail contactEmail)
        {
            if (ModelState.IsValid)
            {
                db.ContactEmails.Add(contactEmail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StoreId = new SelectList(db.Stores, "Id", "Name", contactEmail.StoreId);
            return View(contactEmail);
        }

        // GET: ContactEmails/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactEmail contactEmail = db.ContactEmails.Find(id);
            if (contactEmail == null)
            {
                return HttpNotFound();
            }
            ViewBag.StoreId = new SelectList(db.Stores, "Id", "Name", contactEmail.StoreId);
            return View(contactEmail);
        }

        // POST: ContactEmails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,Name,Body,Title,DateTime,StoreId")] ContactEmail contactEmail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactEmail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StoreId = new SelectList(db.Stores, "Id", "Name", contactEmail.StoreId);
            return View(contactEmail);
        }

        // GET: ContactEmails/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactEmail contactEmail = db.ContactEmails.Find(id);
            if (contactEmail == null)
            {
                return HttpNotFound();
            }
            return View(contactEmail);
        }

        // POST: ContactEmails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ContactEmail contactEmail = db.ContactEmails.Find(id);
            db.ContactEmails.Remove(contactEmail);
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
