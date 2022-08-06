using Matgary.DAL;
using System;
using System.Data;
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
    public class AboutsController : Controller
    {
        private Matgary.DAL.AppContext db = new Matgary.DAL.AppContext();


        // GET: Abouts
        public async Task<ActionResult> Index(string store)
        {
            var storeId = Session["StoreId"]?.ToString();
            var abouts = await db.Abouts.ToListAsync();

            if (!string.IsNullOrEmpty(storeId))
            {
                var currentStoreId = int.Parse(storeId);

                abouts = abouts
                    .Where(a => a.StoreId == currentStoreId).ToList();
            }

            if (!string.IsNullOrEmpty(store))
            {
                var currentStoreId = int.Parse(store);
                abouts = abouts
                    .Where(a => a.StoreId == currentStoreId).ToList();
            }
            ViewBag.Stores = new SelectList(db.Stores, "Id", "Name");

            return View(abouts);
        }

        // GET: Abouts/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = await db.Abouts.FirstOrDefaultAsync(a => a.Id == id);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(About about, HttpPostedFileBase upload)
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
