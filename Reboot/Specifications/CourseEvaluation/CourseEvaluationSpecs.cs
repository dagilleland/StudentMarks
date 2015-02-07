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

namespace StudentMarks.Framework.Specifications.CourseEvaluation
{
    [Story()]
    public class CourseEvaluationSpecs_2
    {
        [Fact]
        public void Should_Handle_AssignCourse()
        {
            Guid aggregateId = Guid.NewGuid();
            object[] givenPriorEvents = new object[0];
            var givenSut = new Course();
            var whenCommand = new AssignCourse(aggregateId, "PROG 1001", "Programming Fundamentals", 50);
            var expectedEvents = new object[] {new CourseAssigned(aggregateId, "PROG 1011", "Programming Fundamentals", 50)};
            var expectedSut = new Course() { Number = "PROG 1001", Name = "Programming Fundamentals", PassMark = 50 };
            IEnumerable<object> actualEvents = null;

            this.Given(_ => ApplyEvents(givenSut, givenPriorEvents))
                .When(_ => DispatchingCommand<AssignCourse>(givenSut, whenCommand, out actualEvents))
                .Then(_ => ThenTheExpectedEventIsGenerated(expectedEvents, actualEvents))
                .BDDfy();
        }
        private void ApplyEvents(Course givenSut, object[] givenPriorEvents)
        {
            givenSut.ApplyEvents(givenPriorEvents);
        }
        private void DispatchingCommand<TCommand>(Course givenSut, TCommand whenCommand, out IEnumerable<object> actualEvents)
        {
            var handler = givenSut as IHandleCommand<TCommand>;
            if (handler == null)
                throw new CommandHandlerNotDefinedException(string.Format(
                    "Aggregate {0} does not yet handle command {1}",
                    givenSut.GetType().Name, whenCommand.GetType().Name));
            IEnumerable<object> actual = null;

            // Expected events, but got exception {0}
            Assert.DoesNotThrow(() => {
                actual = handler.Handle(whenCommand).Cast<object>();
            });

            actualEvents = actual;
        }
        private void ThenTheExpectedEventIsGenerated(IEnumerable<object> expectedEvents, IEnumerable<object> gotEvents)
        {
            if (gotEvents != null)
            {
                if (gotEvents.Count() < expectedEvents.Count())
                    Assert.True(false, string.Format("Expected event(s) missing: {0}",
                        string.Join(", ", EventDiff(expectedEvents.ToArray(), gotEvents.ToArray()))));
                else if (gotEvents.Count() > expectedEvents.Count())
                    Assert.True(false, string.Format("Unexpected event(s) emitted: {0}",
                        string.Join(", ", EventDiff(gotEvents.ToArray(), expectedEvents.ToArray()))));
                else
                {
                    var expectedAndActual = expectedEvents.Zip(gotEvents, (e, a) => new { Expected = e, Actual = a });
                    foreach (var pair in expectedAndActual)
                    {
                        if (pair.Actual.GetType() == pair.Expected.GetType())
                        {
                            string serializeExpected = Serialize(pair.Expected);
                            string serializeActual = Serialize(pair.Actual);
                            Assert.Equal(serializeExpected, serializeActual);
                        }
                        else
                            Assert.True(false, string.Format(
                                "Incorrect event in results; expected a {0} but got a {1}",
                                pair.Expected.GetType().Name, pair.Actual.GetType().Name));
                    }
                }
                //for (var i = 0; i < gotEvents.Count(); i++)
                //    if (gotEvents[i].GetType() == expectedEvents[i].GetType())
                //        Assert.Equal(Serialize(expectedEvents[i]), Serialize(gotEvents[i]));
                //    else
                //        Assert.True(false, string.Format(
                //            "Incorrect event in results; expected a {0} but got a {1}",
                //            expectedEvents[i].GetType().Name, gotEvents[i].GetType().Name));
            }
            //else if (got is CommandHandlerNotDefinedException)
            //    Assert.True(false, (got as Exception).Message);
            //else
            //    Assert.True(false, string.Format(,
            //        got.GetType().Name));
        }

        private string Serialize(object obj)
        {
            var ser = new XmlSerializer(obj.GetType());
            var ms = new MemoryStream();
            ser.Serialize(ms, obj);
            ms.Seek(0, SeekOrigin.Begin);
            return new StreamReader(ms).ReadToEnd();
        }

        private string[] EventDiff(object[] a, object[] b)
        {
            var diff = a.Select(e => e.GetType().Name).ToList();
            foreach (var remove in b.Select(e => e.GetType().Name))
                diff.Remove(remove);
            return diff.ToArray();
        }

        private class CommandHandlerNotDefinedException : Exception
        {
            public CommandHandlerNotDefinedException(string msg) : base(msg) { }
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
