using StudentMarks.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentMarks.Controllers
{
    public class CourseQueryController : ApiController
    {

        // GET: api/CourseQuery/name
        public Course GetByName(string name)
        {
            return null;
        }
    }
    public class CourseCommandController : ApiController
    {
        // GET: api/CourseCommand
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CourseCommand/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CourseCommand
        public HttpResponseMessage Post([FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // PUT: api/CourseCommand/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CourseCommand/5
        public void Delete(int id)
        {
        }
    }
}
