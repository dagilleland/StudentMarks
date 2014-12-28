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
    public class OfferingEvaluationsController : Controller
    {
        private CourseGradesContext db = new CourseGradesContext();

        // GET: OfferingEvaluations
        public ActionResult Index()
        {
            return View(db.OfferingEvaluations.ToList());
        }

        // GET: OfferingEvaluations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferingEvaluation offeringEvaluation = db.OfferingEvaluations.Find(id);
            if (offeringEvaluation == null)
            {
                return HttpNotFound();
            }
            return View(offeringEvaluation);
        }

        // GET: OfferingEvaluations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OfferingEvaluations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id")] OfferingEvaluation offeringEvaluation)
        {
            if (ModelState.IsValid)
            {
                db.OfferingEvaluations.Add(offeringEvaluation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(offeringEvaluation);
        }

        // GET: OfferingEvaluations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferingEvaluation offeringEvaluation = db.OfferingEvaluations.Find(id);
            if (offeringEvaluation == null)
            {
                return HttpNotFound();
            }
            return View(offeringEvaluation);
        }

        // POST: OfferingEvaluations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id")] OfferingEvaluation offeringEvaluation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offeringEvaluation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(offeringEvaluation);
        }

        // GET: OfferingEvaluations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferingEvaluation offeringEvaluation = db.OfferingEvaluations.Find(id);
            if (offeringEvaluation == null)
            {
                return HttpNotFound();
            }
            return View(offeringEvaluation);
        }

        // POST: OfferingEvaluations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OfferingEvaluation offeringEvaluation = db.OfferingEvaluations.Find(id);
            db.OfferingEvaluations.Remove(offeringEvaluation);
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
