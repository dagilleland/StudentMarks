using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentMarksMVC.Models;

namespace StudentMarksMVC.Controllers
{
    public class SubComponentsController : Controller
    {
        private CourseGradesContext db = new CourseGradesContext();

        // GET: SubComponents
        public ActionResult Index()
        {
            return View(db.SubComponents.ToList());
        }

        // GET: SubComponents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubComponent subComponent = db.SubComponents.Find(id);
            if (subComponent == null)
            {
                return HttpNotFound();
            }
            return View(subComponent);
        }

        // GET: SubComponents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SubComponents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Weight,IsBonus,IsPassFail")] SubComponent subComponent)
        {
            if (ModelState.IsValid)
            {
                db.SubComponents.Add(subComponent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subComponent);
        }

        // GET: SubComponents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubComponent subComponent = db.SubComponents.Find(id);
            if (subComponent == null)
            {
                return HttpNotFound();
            }
            return View(subComponent);
        }

        // POST: SubComponents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Weight,IsBonus,IsPassFail")] SubComponent subComponent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subComponent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subComponent);
        }

        // GET: SubComponents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubComponent subComponent = db.SubComponents.Find(id);
            if (subComponent == null)
            {
                return HttpNotFound();
            }
            return View(subComponent);
        }

        // POST: SubComponents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubComponent subComponent = db.SubComponents.Find(id);
            db.SubComponents.Remove(subComponent);
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
