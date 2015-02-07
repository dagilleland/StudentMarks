using Edument.CQRS;
using StudentMarks.Framework.CourseEvaluation.Commands;
using StudentMarks.Framework.CourseEvaluation.Domain;
using StudentMarks.Framework.CourseEvaluation.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;
using Xunit.Extensions;
using System.Collections;

namespace StudentMarks.Framework.Specifications.CourseEvaluation
{
    public class CourseEvaluationSpecs : BDDTest<Course>
    {
        [Theory]
        [InlineData("PROG 1001", "Programming Fundamentals")]
        public void CanAssignCourse(string number, string name)
        {
            Test(
                Given(),
                When(new AssignCourse(name, number)),
                Then(new CourseAssigned(name, number)));
        }
    }
}
