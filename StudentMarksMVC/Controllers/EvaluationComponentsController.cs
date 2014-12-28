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
    public class EvaluationComponentsController : Controller
    {
        private CourseGradesContext db = new CourseGradesContext();

        // GET: EvaluationComponents
        public ActionResult Index()
        {
            return View(db.EvaluationComponents.ToList());
        }

        // GET: EvaluationComponents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluationComponent evaluationComponent = db.EvaluationComponents.Find(id);
            if (evaluationComponent == null)
            {
                return HttpNotFound();
            }
            return View(evaluationComponent);
        }

        // GET: EvaluationComponents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EvaluationComponents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Weight,IsControlled,IsArchived")] EvaluationComponent evaluationComponent)
        {
            if (ModelState.IsValid)
            {
                db.EvaluationComponents.Add(evaluationComponent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(evaluationComponent);
        }

        // GET: EvaluationComponents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluationComponent evaluationComponent = db.EvaluationComponents.Find(id);
            if (evaluationComponent == null)
            {
                return HttpNotFound();
            }
            return View(evaluationComponent);
        }

        // POST: EvaluationComponents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Weight,IsControlled,IsArchived")] EvaluationComponent evaluationComponent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evaluationComponent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evaluationComponent);
        }

        // GET: EvaluationComponents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluationComponent evaluationComponent = db.EvaluationComponents.Find(id);
            if (evaluationComponent == null)
            {
                return HttpNotFound();
            }
            return View(evaluationComponent);
        }

        // POST: EvaluationComponents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EvaluationComponent evaluationComponent = db.EvaluationComponents.Find(id);
            db.EvaluationComponents.Remove(evaluationComponent);
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
