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
    // TODO: Re-instate http://localhost:58955/api/CourseConfig/GetCourseName
    // [Authorize]
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
                if (existing != null)
                {
                    // Update the name
                    existing.Name = name;
                    db.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.Courses.Add(new Course() { Name = name });
                }
                db.SaveChanges();
            }
        }
        [Route("GetCourseName")]
        public string GetCourseName()
        {
            using (var db = AppContext.Create())
            {
                Course info = db.Courses.FirstOrDefault();
                if (info != null)
                    return info.Name;
                else
                    return "Un-named Course";
            }
        }

    }
}
