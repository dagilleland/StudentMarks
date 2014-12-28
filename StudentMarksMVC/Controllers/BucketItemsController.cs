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
    public class BucketItemsController : Controller
    {
        private CourseGradesContext db = new CourseGradesContext();

        // GET: BucketItems
        public ActionResult Index()
        {
            return View(db.BucketItems.ToList());
        }

        // GET: BucketItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BucketItem bucketItem = db.BucketItems.Find(id);
            if (bucketItem == null)
            {
                return HttpNotFound();
            }
            return View(bucketItem);
        }

        // GET: BucketItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BucketItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Pass")] BucketItem bucketItem)
        {
            if (ModelState.IsValid)
            {
                db.BucketItems.Add(bucketItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bucketItem);
        }

        // GET: BucketItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BucketItem bucketItem = db.BucketItems.Find(id);
            if (bucketItem == null)
            {
                return HttpNotFound();
            }
            return View(bucketItem);
        }

        // POST: BucketItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Pass")] BucketItem bucketItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bucketItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bucketItem);
        }

        // GET: BucketItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BucketItem bucketItem = db.BucketItems.Find(id);
            if (bucketItem == null)
            {
                return HttpNotFound();
            }
            return View(bucketItem);
        }

        // POST: BucketItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BucketItem bucketItem = db.BucketItems.Find(id);
            db.BucketItems.Remove(bucketItem);
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
