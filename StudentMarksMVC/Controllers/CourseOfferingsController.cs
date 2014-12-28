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
    public class CourseOfferingsController : Controller
    {
        private CourseGradesContext db = new CourseGradesContext();

        // GET: CourseOfferings
        public ActionResult Index()
        {
            return View(db.CourseOfferings.ToList());
        }

        // GET: CourseOfferings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseOffering courseOffering = db.CourseOfferings.Find(id);
            if (courseOffering == null)
            {
                return HttpNotFound();
            }
            return View(courseOffering);
        }

        // GET: CourseOfferings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseOfferings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StartDate,EndDate")] CourseOffering courseOffering)
        {
            if (ModelState.IsValid)
            {
                db.CourseOfferings.Add(courseOffering);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courseOffering);
        }

        // GET: CourseOfferings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseOffering courseOffering = db.CourseOfferings.Find(id);
            if (courseOffering == null)
            {
                return HttpNotFound();
            }
            return View(courseOffering);
        }

        // POST: CourseOfferings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartDate,EndDate")] CourseOffering courseOffering)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseOffering).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courseOffering);
        }

        // GET: CourseOfferings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseOffering courseOffering = db.CourseOfferings.Find(id);
            if (courseOffering == null)
            {
                return HttpNotFound();
            }
            return View(courseOffering);
        }

        // POST: CourseOfferings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseOffering courseOffering = db.CourseOfferings.Find(id);
            db.CourseOfferings.Remove(courseOffering);
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
