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
    public class MarkedItemsController : Controller
    {
        private CourseGradesContext db = new CourseGradesContext();

        // GET: MarkedItems
        public ActionResult Index()
        {
            return View(db.MarkedItems.ToList());
        }

        // GET: MarkedItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkedItem markedItem = db.MarkedItems.Find(id);
            if (markedItem == null)
            {
                return HttpNotFound();
            }
            return View(markedItem);
        }

        // GET: MarkedItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MarkedItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PossibleMarks,EarnedMark,AssignedMark,Comment")] MarkedItem markedItem)
        {
            if (ModelState.IsValid)
            {
                db.MarkedItems.Add(markedItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(markedItem);
        }

        // GET: MarkedItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkedItem markedItem = db.MarkedItems.Find(id);
            if (markedItem == null)
            {
                return HttpNotFound();
            }
            return View(markedItem);
        }

        // POST: MarkedItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PossibleMarks,EarnedMark,AssignedMark,Comment")] MarkedItem markedItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(markedItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(markedItem);
        }

        // GET: MarkedItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkedItem markedItem = db.MarkedItems.Find(id);
            if (markedItem == null)
            {
                return HttpNotFound();
            }
            return View(markedItem);
        }

        // POST: MarkedItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MarkedItem markedItem = db.MarkedItems.Find(id);
            db.MarkedItems.Remove(markedItem);
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
