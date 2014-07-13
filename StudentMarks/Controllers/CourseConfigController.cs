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
using System.Data.Entity;
using StudentMarks.Models.Entities.DTOs;

namespace StudentMarks.Controllers
{
    // TODO: Re-instate http://localhost:58955/api/CourseConfig/GetCourseName
    // [Authorize]
    [RoutePrefix("api/CourseConfig")]
    public class CourseConfigController : ApiController
    {
        #region Constructors, Fields/Properties, Overrides
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing && SharedContext != null && SharedContext.Database.Connection.State != System.Data.ConnectionState.Closed)
            {
                SharedContext.Dispose();
                SharedContext = null;
            }
        }

        private AppContext SharedContext;
        public CourseConfigController(AppContext context)
        {
            SharedContext = context;
        }
        public CourseConfigController() : this(null) { }
        #endregion

        //private CourseConfigurationManager _configManager;

        #region Public WebAPI Routes
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

        [Route("AddEvaluationComponents")]
        public void AddEvaluationComponents(EvaluationComponent dto)
        {
            
        }

        [Route("GetEvaluationComponents")]
        public EvaluationComponent GetEvaluationComponents()
        {
            using (var db = AppContext.Create())
            {
                List<MarkableItem> components = new List<MarkableItem>();
                foreach (var item in db.Buckets)
                    components.Add(item);
                foreach (var item in db.Quizzes)
                    components.Add(item);
                int bucketWeight = 0;
                var config = db.CourseCongiruations.FirstOrDefault();
                if (config != null)
                    bucketWeight = config.BucketWeight;

                return new EvaluationComponent()
                    {
                        MarkableItems = components.OrderBy(x => x.DisplayOrder).ToList(),
                        BucketTopics = db.Topics.ToList(),
                        BucketWeights = bucketWeight
                    };
            }
        }
        #endregion

        #region Internal Methods
        #region  Queries - CQRS
        internal IList<Topic> GetBucketTopics()
        {
            return SharedContext.Topics.ToList();
        }
        internal IList<Quiz> GetQuizzes()
        {
            return SharedContext.Quizzes.ToList();
        }
        internal IList<Bucket> GetBuckets()
        {
            return SharedContext.Buckets.ToList();
        }
        #endregion

        #region Commands - CQRS
        public void AddCourseConfiguration(CourseConfiguration info)
        {
            SharedContext.CourseCongiruations.Add(info);
        }
        public void AddBuckets(IList<Bucket> info)
        {
            SharedContext.Buckets.AddRange(info);
        }
        public void AddQuizzes(IList<Quiz> info)
        {
            SharedContext.Quizzes.AddRange(info);
        }
        #endregion
        #endregion


    }
}
