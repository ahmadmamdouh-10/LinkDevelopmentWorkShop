using ExcelDataReader;
using Matgary.DAL;
using MatgaryAdmin.Helpers;
using MatgaryAdmin.Models;
using OfficeOpenXml;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Matgary.BLL;

namespace MatgaryAdmin.Controllers
{
    [AuthorizeUser(Roles = "Admin,SuperAdmin")]
    public class ProductsController : Controller
    {

        private Matgary.DAL.AppContext db = new Matgary.DAL.AppContext();


        public ActionResult Index(string category, int? page, string store, string code, string name, int? inStock)
        {
            var storeId = Session["StoreId"]?.ToString();

            var categories = db.Categories.ToList();

            var products = db.Products
                .Include(p => p.ProductCategories);
            if (!string.IsNullOrEmpty(storeId))
            {
                var currentStoreId = long.Parse(storeId);
                products = products.Where(c => c.StoreId == currentStoreId);
                categories = categories.Where(c => c.StoreId == currentStoreId).ToList();
            }

            if (!string.IsNullOrEmpty(category))
            {
                var categoryId = Convert.ToInt32(category);

                //products = products.Where(p => p.CategoryId == categoryId);
                products = products.Where(p => p.ProductCategories.Select(c => c.CategoryId).Contains(categoryId));
            }

            if (!string.IsNullOrEmpty(store))
            {
                var currentStoreId = Convert.ToInt32(store);

                products = products.Where(p => p.StoreId == currentStoreId);
                categories = categories.Where(c => c.StoreId == currentStoreId).ToList();

            }

         
            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.Title.Contains(name));
            }

            if (inStock.HasValue)
            {
                if (inStock != -1)
                {
                    if (inStock == 0)
                    {
                        products = products.Where(p => p.InStock == true);
                    }
                    else if (inStock == 1)
                    {
                        products = products.Where(p => p.InStock == false);
                    }
                }
            }
            ViewBag.Categories = new SelectList(categories, "Id", "Title");

            ViewBag.Stores = new SelectList(db.Stores, "Id", "Name");

            var prods = products.ToList();
            for (int i = 0; i < prods.Count(); i++)
            {
                prods[i].Description = prods[i].Description != null ? Regex.Replace(prods[i].Description, @"<[^>]*>", "") : "";
            }
            ViewBag.OrderProds = db.OrderProducts.ToList();
            return View(products.OrderBy(p => p.Id).ToPagedList(page ?? 1, pageSize: Constants.PageSize));
        }



        [HttpGet]
        public ActionResult GetCategories(int storeId)
        {
            var categories = db.Categories.ToList();
            if (storeId != 0)
                categories = categories.Where(c => c.StoreId == storeId).ToList();

            return Json(categories, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        public ActionResult Create()
        {
            var storeId = (long)Session["StoreId"];
            ViewBag.CategoryId = new SelectList(db.Categories.Where(c => c.StoreId == storeId), "Id", "Title");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken, ValidateInput(false)]
        //public ActionResult Create(Product product, HttpPostedFileBase upload, HttpPostedFileBase upload1, HttpPostedFileBase upload2)
        public ActionResult Create(Product product, HttpPostedFileBase upload)
        {
            product.StoreId = (long)Session["StoreId"];
            if (ModelState.IsValid)
            {
                string x = addImage(upload, product.Id, product.StoreId);


                product.InStock = true;
         
                if (x != "")
                {
                    product.ImageUrl = x;
                    db.Products.Add(product);
                }
           
                else
                {
                    product.ImageUrl = null;

                }


                db.Products.Add(product);

                foreach (var catId in product.CategoryIds)
                {
                    db.ProductCategories.Add(new ProductCategory()
                    {
                        CategoryId = catId,
                        ProductId = product.Id
                    });
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }


            ViewBag.CategoryId = new SelectList(db.Categories.Where(c => c.StoreId == product.StoreId), "Id", "Title");
            return View(product);
        }

        public string addImage(HttpPostedFileBase upload, long numberofNewproduct, long storeId)
        {
            if (upload == null)
                return "";

            string fileName = Path.Combine(Server.MapPath("~/Content/images"), upload.FileName);

            upload.SaveAs(fileName);
            Matgary.DAL.Image image = new Matgary.DAL.Image();
            image.ImageUrl = upload.FileName;
            image.ProductId = numberofNewproduct;
            image.StoreId = storeId;
            db.Images.Add(image);

            return upload.FileName;
        }

        public ActionResult Edit(long? id)
        {
            //var storeId = (long)Session["StoreId"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            product.CategoryIds = product.ProductCategories.Select(p => p.CategoryId).ToList();
            if (product == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CategoryId = new SelectList(db.Categories.Where(c=>c.StoreId == storeId), "Id", "TitleA");
            ViewBag.Categories = db.Categories.Where(c => c.StoreId == product.StoreId).ToList();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken, ValidateInput(false)]
        //public ActionResult Edit(Product product, HttpPostedFileBase upload, HttpPostedFileBase upload1, HttpPostedFileBase upload2, FormCollection form)
        public ActionResult Edit(Product product, HttpPostedFileBase upload, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                string x = editImage(0, upload, product.Id, product.StoreId);

                product.InStock = true;
                if (x != "")
                {
                    product.ImageUrl = x;
                }
            
                var productCats = db.ProductCategories.Where(pc => pc.ProductId == product.Id);
                db.ProductCategories.RemoveRange(productCats);
                foreach (var catId in product.CategoryIds)
                {
                    db.ProductCategories.Add(new ProductCategory()
                    {
                        CategoryId = catId,
                        ProductId = product.Id
                    });
                }
                product.DateTime = DateTime.Now;
                db.Entry(product).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.CategoryId = new SelectList(db.Categories.Where(c=>c.StoreId == product.StoreId), "Id", "TitleA");
            ViewBag.Categories = db.Categories.Where(c => c.StoreId == product.StoreId).ToList();
            return View(product);
        }


        public string editImage(int numberoFphoto, HttpPostedFileBase upload, long numberofNewproduct, long storeId)
        {
            if (upload == null)
                return "";

            string fileName = Path.Combine(Server.MapPath("~/Content/images"), upload.FileName);
            //var fileName = WebConfigurationManager.AppSettings["imagePath"] + time + upload.FileName;
            upload.SaveAs(fileName);

            var image = db.Images.Where(I => I.ProductId == numberofNewproduct).ToList();
            if (image.Count == 0 || numberoFphoto == image.Count)
            {
                Matgary.DAL.Image image1 = new Matgary.DAL.Image();
                image1.DateTime = DateTime.Now;
                image1.ProductId = numberofNewproduct;
                image1.ImageUrl = upload.FileName;
                image1.StoreId = storeId;
                db.Images.Add(image1);
                return upload.FileName;
            }
            else
            {
                image[numberoFphoto].ImageUrl = upload.FileName;
                db.Entry(image[numberoFphoto]).State = EntityState.Modified;
                return upload.FileName;
            }
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Product product = db.Products.Find(id);
            var offer = db.Offers.Where(a => a.ProductId == id).ToList();
            for (int i = 0; i < offer.Count; i++)
            {
                Offer offerdeleted = offer[i];
                if (offerdeleted.ProductId == product.Id)
                {
                    db.Offers.Remove(offerdeleted);
                    db.SaveChanges();
                }
            }

            db.Products.Remove(product);
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

    public class ProductReportVM
    {
        public string Title { get; set; }
        public int Rate { get; set; }
        public int Reviews { get; set; }
        public int FavoriteCount { get; set; }
        public int Total { get; set; }
        public string Store { get; set; }

    }
}


