using NTestDataBuilder;
using StudentMarks.Controllers;
using StudentMarks.Models;
using StudentMarks.Models.Entities;
using StudentMarks.Models.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMark.Requirements.TestData
{

    public static partial class ObjectBuilderMother
    {
        // CREDITS: http://robdmoore.id.au/blog/2013/05/26/test-data-generation-the-right-way-object-mother-test-data-builders-nsubstitute-nbuilder/
        // CREDITS: http://martinfowler.com/bliki/ObjectMother.html
        public static class Courses
        {
            public static CourseBuilder NewWithNameOnly
            {
                get { return new CourseBuilder().WithName("New course with Name only"); }
            }
        }

        public static class WebApiControllers
        {
            public static CourseQueryController CourseQuery {get{return new CourseQueryController();}}
            public static CourseCommandController CourseCommand { get { return new CourseCommandController(); } }
        }
    }
    public class WebApiControllerBuilder : TestDataBuilder<System.Web.Http.ApiController, WebApiControllerBuilder>
    {
        public WebApiControllerBuilder()
        {
            ForUri("http://localhost");
            Set(x => x.Configuration, new System.Web.Http.HttpConfiguration());
            //Get(x => x.Configuration).Routes.Map
        }
        public WebApiControllerBuilder ForUri(string uri)
        {
            Set(x => x.Request, new System.Net.Http.HttpRequestMessage { RequestUri = new Uri(uri) });
            return this;
        }

        public TController BuildObject<TController>() where TController : System.Web.Http.ApiController
        {
            var result = default(TController);
            result.Request = Get(x => x.Request);
            result.Configuration = Get(x => x.Configuration);
            result.RequestContext = Get(x => x.RequestContext);
            return result;
        }

        protected override System.Web.Http.ApiController BuildObject()
        {
            throw new NotSupportedException("Use the generic BuildObject<TSubClass>");
        }
    }

    public class CourseBuilder : TestDataBuilder<Course, CourseBuilder>
    {
        public CourseBuilder()
        {
            WithName("Empty existing course").WithId(1);
        }
        public CourseBuilder WithName(string name)
        { Set(x => x.Name, name); return this; }
        public CourseBuilder WithId(int id)
        { Set(x => x.CourseID, id); return this; }

        protected override Course BuildObject()
        { return new Course() { Name = Get(x => x.Name), CourseID = Get(x => x.CourseID) }; }
    }
    public class EvaluationComponentBuilder : TestDataBuilder<EvaluationComponent, EvaluationComponentBuilder>
    {

        protected override EvaluationComponent BuildObject()
        {
            return new EvaluationComponent(
                Get(x => x.Title),
                Get(x => x.Weight),
                Get(x => x.IsControlled));
        }

        public EvaluationComponentBuilder WithTitle(string title)
        {
            Set(x => x.Title, title);
            return this;
        }
        public EvaluationComponentBuilder WithWeight(int weight)
        {
            Set(x => x.Weight, weight);
            return this;
        }
    }
    public class StudentBuilder : TestDataBuilder<Student, StudentBuilder>
    {
        public StudentBuilder()
        {
            WithFirstName("Stewart");
            WithLastName("Dent");
            WithSchoolId(1234567);
        }

        public StudentBuilder WithFirstName(string first)
        {
            Set(x => x.FirstName, first);
            return this;
        }

        public StudentBuilder WithLastName(string last)
        {
            Set(x => x.LastName, last);
            return this;
        }

        public StudentBuilder WithSchoolId(long schoolId)
        {
            Set(x => x.SchoolID, schoolId);
            return this;
        }

        protected override Student BuildObject()
        {
            return new Student()
            {
                FirstName = Get(x => x.FirstName),
                LastName = Get(x => x.LastName),
                SchoolID = Get(x => x.SchoolID)
            };
        }
    }
}
