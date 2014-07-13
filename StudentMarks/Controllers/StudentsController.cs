using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using StudentMarks.Models;
using StudentMarks.Models.Entities;
using StudentMarks.App.DAL;

namespace StudentMarks.Controllers
{
    public class StudentsController : ApiController
    {
        //private AppContext db = AppContext.Create();

        // GET: api/Students
        public IList<Student> GetStudents()
        {
            using (var db = AppContext.Create())
            {
                return db.Students.ToList();
            }
        }

        // GET: api/Students/5
        [ResponseType(typeof(Student))]
        public Student GetStudent(int id)
        {
            using (var db = AppContext.Create())
            {
                Student student = db.Students.Find(id);
                if (student == null)
                {
                    return null;
                }

                return student;
            }
        }
        //[ResponseType(typeof(Student))]
        //public IHttpActionResult GetStudent(int id)
        //{
        //    using (var db = AppContext.Create())
        //    {
        //        Student student = db.Students.Find(id);
        //        if (student == null)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(student);
        //    }
        //}

        // PUT: api/Students/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudent(int id, Student student)
        {
            using (var db = AppContext.Create())
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != student.StudentID)
                {
                    return BadRequest();
                }

                db.Entry(student).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        // POST: api/Students
        [ResponseType(typeof(Student))]
        public Student PostStudent(Student student)
        {
            using (var db = AppContext.Create())
            {
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}

                db.Students.Add(student);
                db.SaveChanges();

                return student;
            }
        }
        //[ResponseType(typeof(Student))]
        //public IHttpActionResult PostStudent(Student student)
        //{
        //    using (var db = AppContext.Create())
        //    {
        //        //if (!ModelState.IsValid)
        //        //{
        //        //    return BadRequest(ModelState);
        //        //}

        //        db.Students.Add(student);
        //        db.SaveChanges();

        //        return CreatedAtRoute("DefaultApi", new { id = student.Id }, student);
        //    }
        //}

        // DELETE: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult DeleteStudent(int id)
        {
            using (var db = AppContext.Create())
            {

                Student student = db.Students.Find(id);
                if (student == null)
                {
                    return NotFound();
                }

                db.Students.Remove(student);
                db.SaveChanges();

                return Ok(student);
            }
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        //db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool StudentExists(int id)
        {
            using (var db = AppContext.Create())
            {
                return db.Students.Count(e => e.StudentID == id) > 0;
            }
        }
    }
}