using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using StudentMarks.Models;
using StudentMarks.Providers;
using StudentMarks.Results;
using StudentMarks.App;
using StudentMarks.App.DAL;
using StudentMarks.Models.Entities;

namespace StudentMarks.Controllers
{
    [Authorize]
    [RoutePrefix("api/CourseConfig")]
    public class CourseConfigController : ApiController
    {
        //private CourseConfigurationManager _configManager;

        // POST api/CourseConfig/SetCourseName
        [Route("SetCourseName")]
        public void SetCourseName(string name)
        {
            using (var db = AppContext.Create())
            {
                var existing = db.Courses.FirstOrDefault();
                if (existing.Id > 0)
                {
                    // Update the name
                    existing.Name = name;
                    db.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.Courses.Add(existing);
                }
                db.SaveChanges();
            }
        }
        public string GetCourseName()
        {
            using (var db = AppContext.Create())
            {
                return db.Courses.FirstOrDefault().Name;
            }
        }

    }
}
