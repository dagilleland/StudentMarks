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
        [HttpPost]
        [Route("SetCourseName")]
        //[ActionName("Simple")]
        public void SetCourseName([FromBody] string name)
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

        // GET api/CourseConfig/GetCourseName
        [HttpGet]
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

        // POST api/CourseConfig/SaveEvaluationComponents
        [HttpPost]
        [Route("SaveEvaluationComponents")]
        public void SaveEvaluationComponents([FromBody]EvaluationComponent dto)
        {
            using (var db = AppContext.Create())
            {
                var config = db.CourseConfiruations.FirstOrDefault();
                if (config == null)
                    db.CourseConfiruations.Add(new CourseConfiguration() { BucketWeight = dto.BucketWeights });
                else
                {
                    config.BucketWeight = dto.BucketWeights;
                    db.CourseConfiruations.Attach(config);
                    db.Entry(config).State = EntityState.Modified;
                }

                var newTopics = dto.BucketTopics;
                var existing = db.Topics.ToList();
                foreach (var item in existing)
                    if (!newTopics.Exists(x => x.Description == item.Description))
                        db.Topics.Remove(item);
                foreach (var item in newTopics)
                    if (!existing.Exists(x => x.Description == item.Description))
                        db.Topics.Add(item);

                db.SaveChanges();
                existing = db.Topics.ToList();

                var quizzes = (from item in dto.MarkableItems
                               where item.ItemType.Equals("Quiz")
                               select new Quiz()
                               {
                                   DisplayOrder = item.DisplayOrder,
                                   Name = item.Name,
                                   Weight = item.Weight.Value,
                                   PotentialMarks = item.TotalPossibleMarks.HasValue ? item.TotalPossibleMarks.Value : 0,
                                   Id = item.ID.HasValue ? item.ID.Value : 0
                               }).ToList();
                List<Quiz> dbQuizzesToList = db.Quizzes.ToList();
                foreach (var item in dbQuizzesToList)
                    if (!quizzes.Exists(x => x.Id == item.Id))
                        db.Quizzes.Remove(item);
                foreach (var item in quizzes)
                {
                    var found = dbQuizzesToList.FirstOrDefault(x => x.Id == item.Id);
                    if (found == null)
                        db.Quizzes.Add(item);
                    else
                    {
                        found = db.Quizzes.Find(item.Id);
                        found.Name = item.Name;
                        found.PotentialMarks = item.PotentialMarks;
                        found.Weight = item.Weight;
                        found.DisplayOrder = item.DisplayOrder;
                        db.Quizzes.Attach(found);
                        db.Entry(found).State = EntityState.Modified;
                    }
                }

                var buckets = (from item in dto.MarkableItems
                               where item.ItemType.Equals("Bucket")
                               select new Bucket()
                               {
                                   DisplayOrder = item.DisplayOrder,
                                   Name = item.Name,
                                   Weight = dto.BucketWeights,
                                   Id = item.ID.HasValue ? item.ID.Value : 0,
                                   TopicID = existing.Single(x => x.Description == item.Topic).TopicID
                               }).ToList();
                List<Bucket> dbBucketsToList = db.Buckets.ToList();
                foreach (var item in dbBucketsToList)
                    if (!buckets.Exists(x => x.Id == item.Id))
                        db.Buckets.Remove(item);
                foreach (var item in buckets)
                {
                    var found = dbBucketsToList.FirstOrDefault(x=>x.Id == item.Id);
                    if (found == null)
                        db.Buckets.Add(item);
                    else
                    {
                        found = db.Buckets.Find(item.Id);
                        found.Name = item.Name;
                        found.TopicID = item.TopicID;
                        found.Weight = item.Weight;
                        found.DisplayOrder = item.DisplayOrder;
                        db.Buckets.Attach(found);
                        db.Entry(found).State = EntityState.Modified;
                    }
                }

                db.SaveChanges();
            }
        }

        // GET api/CourseConfig/GetEvaluationComponents
        [HttpGet]
        [Route("GetEvaluationComponents")]
        public EvaluationComponent GetEvaluationComponents()
        {
            using (var db = AppContext.Create())
            {
                List<Component> components = new List<Component>();
                foreach (var item in db.Buckets.Include("Topic"))
                    components.Add(new Component(item));
                foreach (var item in db.Quizzes)
                    components.Add(new Component(item));
                int bucketWeight = 0;
                var config = db.CourseConfiruations.FirstOrDefault();
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
            SharedContext.CourseConfiruations.Add(info);
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
