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
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;

namespace StudentMarks.Framework.Specifications.CourseEvaluation
{
    [Story()]
    public class CourseEvaluationSpecs_2 : AbstractCQRSDomainEventFixture<Course>
    {
        [Fact]
        public void Should_Handle_AssignCourse()
        {
            Guid aggregateId = Guid.NewGuid();
            object[] givenPriorEvents = new object[0];
            var givenSut = new Course();
            var whenCommand = new AssignCourse(aggregateId, "PROG 1001", "Programming Fundamentals", 50);
            var expectedEvents = new object[] {new CourseAssigned(aggregateId, "PROG 1001", "Programming Fundamentals", 50)};
            var expectedSut = new Course() { Number = "PROG 1001", Name = "Programming Fundamentals", PassMark = 50 };
            IEnumerable<object> actualEvents = null;

            this.Given(_ => GivenPriorEvents(givenSut, givenPriorEvents), false)
                .When(_ => WhenDispatchingCommand<AssignCourse>(givenSut, whenCommand, out actualEvents))
                .Then(_ => ThenExpectedEventsAreGenerated(expectedEvents, actualEvents))
                .BDDfy();
        }
    }

    public class CourseEvaluationSpecs : BDDTest<Course>
    {
        #region Primary Domain Scenario
        [Theory]
        [InlineData("PROG 1001", "Programming Fundamentals", 50)]
        [InlineData("COMP 2018", "Application Development", 70)]
        public void Handles_AssignCourse(string number, string name, int passMark)
        {
            Guid aggregateId = Guid.NewGuid();
            Test(
                Given(),
                When(new AssignCourse(aggregateId, number, name, passMark)),
                Then(new CourseAssigned(aggregateId, number, name, passMark)));
        }
        #endregion

        #region Secondary Domain Scenarios
        [Theory]
        [InlineData("PROG 1001", "Programming Fundamentals")]
        [InlineData("COMP 2018", "Application Development")]
        public void Rejects_AssignCourse_With_Duplicate_Name(string number, string name)
        {
            // TODO: Build exception environment
            Test(
                Given(),
                When(new AssignCourse(Guid.Empty, number, name, 0)),
                ThenFailWith<CourseDuplication>());
        }

        [Theory]
        [InlineData("PROG 1001", "Programming Fundamentals")]
        [InlineData("COMP 2018", "Application Development")]
        public void Rejects_AssignCourse_With_Duplicate_Number(string number, string name)
        {
            // TODO: Build exception environment
            Test(
                Given(),
                When(new AssignCourse(Guid.Empty, number, name, 0)),
                ThenFailWith<CourseDuplication>());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void Rejects_AssignCourse_With_Invalid_Name(string name)
        {
            Test(
                Given(),
                When(new AssignCourse(Guid.Empty, "PROG 1001", name, 0)),
                ThenFailWith<CourseNameInvalid>());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void Rejects_AssignCourse_With_Invalid_Number(string number)
        {
            // TODO: Build exception environment
            Test(
                Given(),
                When(new AssignCourse(Guid.Empty, number, "Programming Fundamentals", 0)),
                ThenFailWith<CourseNumberInvalid>());
        }
        #endregion
    }
}
