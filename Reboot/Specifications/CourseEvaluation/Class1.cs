using StudentMarks.Framework.CourseEvaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;

namespace StudentMarks.Framework.Specifications.CourseEvaluation
{
    [Story(AsA="Instructor",
        IWant="I want to set up courses", SoThat="")]
    public class Class1
    {
        [Fact]
        public void ListCourses()
        {
            List<Course> courses = new List<Course>();
            courses.Add(new Course("COMP101", "Programming Fundamentals"));
            courses.Add(new Course("COMP314", "Domain Driven Design"));
            courses.Add(new Course("COMP325", "CQRS Introduction"));
            this.Given(_ => GivenSetOfCourses(courses))
                .When(_ => WhenListingTheCourses())
                .Then(_ => ThenTheCoursesAreRetreived(courses));
        }

        private void GivenSetOfCourses(List<Course> courses)
        {
            throw new NotImplementedException();
        }
        private void WhenListingTheCourses()
        {
            throw new NotImplementedException();
        }
        private void ThenTheCoursesAreRetreived(List<Course> courses)
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ListCoursesWithOfferings()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void AddCourse()
        {
            this.When(_ => WhenAddingACourse("COMP101", "Programming Fundamentals"))
                .Then(_ => ThenTheCourseIsReceived("COMP101", "Programming Fundamentals"))
                .BDDfy();
        }
        private void WhenAddingACourse(string courseNumber, string courseName)
        {
            
        }
        private void ThenTheCourseIsReceived(string courseNumber, string courseName)
        {
            
        }


    }
}
