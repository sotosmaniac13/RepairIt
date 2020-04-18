using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RepairIt.Models;

namespace RepairIt.Controllers
{
    public class RepairCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RepairCategories
        [ChildActionOnly]
        public ActionResult GetRepairCategories()
        {
            return PartialView("_RepairCategoriesPartial", db.RepairCategories.Include(c => c.Cat_DefTasks).ToList());
        }

        // GET: RepairCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairCategory repairCategory = db.RepairCategories.Find(id);
            if (repairCategory == null)
            {
                return HttpNotFound();
            }
            return View(repairCategory);
        }

        // GET: RepairCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RepairCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cat_Id,Cat_Name")] RepairCategory repairCategory)
        {
            if (ModelState.IsValid)
            {
                db.RepairCategories.Add(repairCategory);
                db.SaveChanges();
                return RedirectToAction("Settings","Home");
            }

            return View(repairCategory);
        }

        // GET: RepairCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairCategory repairCategory = db.RepairCategories.Find(id);
            if (repairCategory == null)
            {
                return HttpNotFound();
            }
            return View(repairCategory);
        }

        // POST: RepairCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Cat_Id,Cat_Name")] RepairCategory repairCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repairCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Settings", "Home");
            }
            return View(repairCategory);
        }

        public PartialViewResult CreateAddEditTasksModal(int? id)
        {
            var newTask = new CategoryTask()
            {
                CatTask_CategoryOfThisTask = db.RepairCategories.Find(id)
            };
            return PartialView("_AddEditTaskPartial", newTask);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditCategoryTasks(CategoryTask task)
        {
            var taskCategory = db.RepairCategories.Find(task.CatTask_CategoryOfThisTask.Cat_Id);
            task.CatTask_CategoryOfThisTask = taskCategory;

            if (ModelState.IsValid)
            {
                if (task.CatTask_Id == 0)
                    db.CategoryTasks.Add(task);
                else
                    db.Entry(task).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Settings","Home");
            }
            return RedirectToAction("Settings", "Home");
        }



        // GET: RepairCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairCategory repairCategory = db.RepairCategories.Find(id);
            if (repairCategory == null)
            {
                return HttpNotFound();
            }
            return View(repairCategory);
        }

        // POST: RepairCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RepairCategory repairCategory = db.RepairCategories.Find(id);
            db.RepairCategories.Remove(repairCategory);
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
