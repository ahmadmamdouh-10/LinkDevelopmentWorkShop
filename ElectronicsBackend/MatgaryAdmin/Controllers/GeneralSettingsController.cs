using Matgary.DAL;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using AppContext = Matgary.DAL.AppContext;

namespace MatgaryAdmin.Controllers
{
    [AuthorizeUser(Roles = "Admin,SuperAdmin")]
    public class GeneralSettingsController : Controller
    {
        private AppContext db = new AppContext();

        // GET: GeneralSettings
        public async Task<ActionResult> Index(string store)
        {
            var storeId = Session["StoreId"]?.ToString();
            var settings = await db.GeneralSettings.ToListAsync();

            if (!string.IsNullOrEmpty(storeId))
            {
                var currentStoreId = long.Parse(storeId);
                settings = settings.Where(c => c.StoreId == currentStoreId).ToList();
            }
            if (!string.IsNullOrEmpty(store))
            {
                var currentStoreId = Convert.ToInt32(store);

                settings = settings.Where(p => p.StoreId == currentStoreId).ToList();
            }

            ViewBag.Stores = new SelectList(db.Stores, "Id", "Name");

            return View(settings);
        }

        // GET: GeneralSettings/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralSetting generalSetting = await db.GeneralSettings.FindAsync(id);
            if (generalSetting == null)
            {
                return HttpNotFound();
            }
            return View(generalSetting);
        }

        // GET: GeneralSettings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GeneralSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GeneralSetting generalSetting)
        {
            if (ModelState.IsValid)
            {
                db.GeneralSettings.Add(generalSetting);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(generalSetting);
        }

        // GET: GeneralSettings/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralSetting generalSetting = await db.GeneralSettings.FindAsync(id);
            if (generalSetting == null)
            {
                return HttpNotFound();
            }
            return View(generalSetting);
        }

        // POST: GeneralSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GeneralSetting generalSetting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(generalSetting).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(generalSetting);
        }

        // GET: GeneralSettings/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralSetting generalSetting = await db.GeneralSettings.FindAsync(id);
            if (generalSetting == null)
            {
                return HttpNotFound();
            }
            return View(generalSetting);
        }

        // POST: GeneralSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            GeneralSetting generalSetting = await db.GeneralSettings.FindAsync(id);
            db.GeneralSettings.Remove(generalSetting);
            await db.SaveChangesAsync();
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
