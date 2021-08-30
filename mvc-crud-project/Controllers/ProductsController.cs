using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using mvc_crud_project.Models;
using PagedList;
using PagedList.Mvc;

namespace mvc_crud_project.Controllers
{
    public class ProductsController : Controller
    {
        private ProductContext db = new ProductContext();

        // Display product list
        public ActionResult Index(int? page)
        {
            var data = db.Products.Include(p => p.Category).ToList();
            return View(data.ToPagedList(page ?? 1,5));
        }

        // Render create product view
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // Add new product
        [HttpPost]
        public ActionResult Create(Product pdata)
        {
            if (ModelState.IsValid == true)
            {
                db.Products.Add(pdata);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", pdata.CategoryId);
            return View(pdata);
        }

        // Get product id to edit
        public ActionResult Edit(int id)
        {
            //var row = db.Products.Where(model => model.Id == id).FirstOrDefault();
            Product pdata = db.Products.Where(model => model.Id == id).FirstOrDefault();
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", pdata.CategoryId);
            return View(pdata);
        }

        // Edit product by id
        [HttpPost]
        public ActionResult Edit(Product pdata)
        {
            if (ModelState.IsValid == true)
            {
                db.Entry(pdata).State = EntityState.Modified;
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", pdata.CategoryId);
            return View(pdata);
        }

        // Delete product by id
        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                var productrow = db.Products.Where(model => model.Id == id).FirstOrDefault();

                if (productrow != null)
                {
                    db.Entry(productrow).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}