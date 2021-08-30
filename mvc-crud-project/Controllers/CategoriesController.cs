using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using mvc_crud_project.Models;

namespace mvc_crud_project.Controllers
{
    public class CategoriesController : Controller
    {
        ProductContext db = new ProductContext();

        // Display category list
        public ActionResult Index()
        {
            var data = db.Categories.ToList();
            return View(data);
        }

        // Render create category view
        public ActionResult Create()
        {
            return View();
        }

        // Add new category
        [HttpPost]
        public ActionResult Create(Category cdata)
        {
            if (ModelState.IsValid == true)
            {
                db.Categories.Add(cdata);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cdata);
        }

        // Get category id to edit
        public ActionResult Edit(int id)
        {
            var row = db.Categories.Where(model => model.Id == id).FirstOrDefault();
            return View(row);
        }

        // Edit category by id
        [HttpPost]
        public ActionResult Edit(Category cdata)
        {
            if (ModelState.IsValid == true)
            {
                db.Entry(cdata).State = EntityState.Modified;
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Index");
            }

            return View(cdata);
        }

        // Delete category by id
        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                var categoryrow = db.Categories.Where(model => model.Id == id).FirstOrDefault();

                if (categoryrow != null)
                {
                    db.Entry(categoryrow).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}