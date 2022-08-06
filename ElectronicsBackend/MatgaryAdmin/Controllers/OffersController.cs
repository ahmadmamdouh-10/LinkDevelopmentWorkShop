using Matgary.DAL;
using OfficeOpenXml;
using System;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
namespace MatgaryAdmin.Controllers
{
    [AuthorizeUser(Roles = "Admin,SuperAdmin")]
    public class OffersController : Controller
    {
        private Matgary.DAL.AppContext db = new Matgary.DAL.AppContext();
        public ActionResult Index(string store)
        {
            var storeId = Session["StoreId"]?.ToString();

            var offers = db.Offers
                .Include(o => o.Category)
                .Include(o => o.Product);

            if (!string.IsNullOrEmpty(storeId))
            {
                var currentStoreId = long.Parse(storeId);
                offers = offers.Where(c => c.StoreId == currentStoreId);
            }

            var offer = offers.ToList();
            for (int i = 0; i < offer.Count(); i++)
            {
                offer[i].Description = offer[i].Description != null ? Regex.Replace(offer[i].Description, @"<[^>]*>", "") : "";
            }
            if (!string.IsNullOrEmpty(store))
            {
                var currentStoreId = Convert.ToInt32(store);

                offer = offer.Where(p => p.StoreId == currentStoreId).ToList();
            }

            ViewBag.Stores = new SelectList(db.Stores, "Id", "Name");

            return View(offer);
        }

        [HttpGet]
        public ActionResult Report(int?id)
        {
            var storeId = Session["StoreId"]?.ToString();

            var offers = db.Offers
                .Include(o => o.Category)
                .Include(o => o.Product);

            if (!string.IsNullOrEmpty(storeId))
            {
                var currentStoreId = long.Parse(storeId);
                offers = offers.Where(c => c.StoreId == currentStoreId);
            }
            ViewBag.Stores = new SelectList(db.Stores, "Id", "Name");

            if (id.HasValue && id.Value !=0)
            {
                offers = offers.Where(c => c.StoreId == id.Value);
            }
            var offer = offers.ToList();
            for (int i = 0; i < offer.Count(); i++)
            {
                offer[i].Description = offer[i].Description != null ? Regex.Replace(offer[i].Description, @"<[^>]*>", "") : "";
            }
            ViewBag.products = new SelectList(db.Products.ToList(), "Id", "TitleE");


            ViewBag.Offers = string.Join(",", offers.Select(o => o.Title));
            ViewBag.Usage = string.Join(",", offers.Select(o => o.RequestedCount.HasValue ? o.RequestedCount.Value : 0));
            ViewBag.MaxUsageNumber = offers.Any() ? offers
                .Max(o => o.RequestedCount.HasValue ? o.RequestedCount.Value : 0) : 0;

            return View(offer);
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        public ActionResult Create()
        {
            var storeId = Session["StoreId"]?.ToString();
            var products = db.Products.ToList();
            if (!string.IsNullOrEmpty(storeId))
            {
                var currentStoreId = long.Parse(storeId);
                products = products.Where(c => c.StoreId == currentStoreId).ToList();
            }
            ViewBag.ProductId = new SelectList(products, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Offer offer)
        {
            offer.StoreId = (long)Session["StoreId"];
            
            if (ModelState.IsValid)
            {

                //new
                offer.OfferType = OfferType.Product;
                Product product = db.Products.Find(offer.ProductId);
                if (product == null)
                {
                    var storeId = Session["StoreId"]?.ToString();
                    var products = db.Products.ToList();
                    if (!string.IsNullOrEmpty(storeId))
                    {
                        var currentStoreId = long.Parse(storeId);
                        products = products.Where(c => c.StoreId == currentStoreId).ToList();
                    }
                    ViewBag.ProductId = new SelectList(products, "Id", "TitleA", offer.ProductId);
                    return View(offer);
                }
                product.Discount = offer.Discount;
                db.Entry(product).State = EntityState.Modified;


                db.Offers.Add(offer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            var tstoreId = Session["StoreId"]?.ToString();
            var tproducts = db.Products.ToList();
            if (!string.IsNullOrEmpty(tstoreId))
            {
                var currentStoreId = long.Parse(tstoreId);
                tproducts = tproducts.Where(c => c.StoreId == currentStoreId).ToList();
            }
            ViewBag.ProductId = new SelectList(tproducts, "Id", "Title", offer.ProductId);

            return View(offer);
        }


        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CategoryId = new SelectList(db.Categories, "Id", "TitleA", offer.CategoryId);
            var storeId = Session["StoreId"]?.ToString();
            var products = db.Products.ToList();
            if (!string.IsNullOrEmpty(storeId))
            {
                var currentStoreId = long.Parse(storeId);
                products = products.Where(c => c.StoreId == currentStoreId).ToList();
            }
            ViewBag.ProductId = new SelectList(products, "Id", "Title", offer.ProductId);
            return View(offer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Offer offer)
        {
     
            if (ModelState.IsValid)
            {
                //remove discount from the old product
                Offer OldOffer = db.Offers.FirstOrDefault(o => o.Id == offer.Id);
                if(OldOffer!= null)
                {

                    Product OldProduct = db.Products.Find(OldOffer.ProductId);
                    if(OldProduct != null)
                    {
                        OldProduct.Discount = 0;
                        db.Entry(OldProduct).State = EntityState.Modified;
                        db.Entry(OldOffer).State = EntityState.Deleted;
                    }
                }

                //new 
                offer.OfferType = OfferType.Product;
              
                // update product discount
                Product product = db.Products.Find(offer.ProductId);
                if (product == null)
                {
                    var storeId = Session["StoreId"]?.ToString();
                    var products = db.Products.ToList();
                    if (!string.IsNullOrEmpty(storeId))
                    {
                        var currentStoreId = long.Parse(storeId);
                        products = products.Where(c => c.StoreId == currentStoreId).ToList();
                    }
                    ViewBag.ProductId = new SelectList(products, "Id", "Title", offer.ProductId);
                    return View(offer);
                }
                product.Discount = offer.Discount;
                db.Entry(product).State = EntityState.Modified;
  
                offer.DateTime = DateTime.Now;
                db.Entry(offer).State = EntityState.Added;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            var tstoreId = Session["StoreId"]?.ToString();
            var tproducts = db.Products.ToList();
            if (!string.IsNullOrEmpty(tstoreId))
            {
                var currentStoreId = long.Parse(tstoreId);
                tproducts = tproducts.Where(c => c.StoreId == currentStoreId).ToList();
            }
            ViewBag.ProductId = new SelectList(tproducts, "Id", "TitleA", offer.ProductId);
            return View(offer);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {

                return HttpNotFound();
            }
            return View(offer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Offer offer = db.Offers.Find(id);
            var prod = OfferType.Product;
            if (offer.OfferType == prod)
            {
                // Delete product discount
                Product product = db.Products.Find(offer.ProductId);
                if (product != null)
                {
                    product.Discount = 0;
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else
            {
            
            }
            db.Offers.Remove(offer);
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
