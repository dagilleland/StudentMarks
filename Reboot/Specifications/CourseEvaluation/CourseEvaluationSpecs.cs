﻿using Edument.CQRS;
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
    public class CourseEvaluationSpecs : AbstractCQRSDomainEventFixture<Course>
    {
        #region Setup
        public Guid AggregateId { get; set; }
        public CourseEvaluationSpecs()
        {
            AggregateId = Guid.NewGuid();
        }
        #endregion

        #region AssignCourse command & CourseAssigned event
        #region Primary Domain Scenario
        [Theory]
        [InlineData("PROG 1001", "Programming Fundamentals", 50)]
        [InlineData("COMP 2018", "Application Development", 70)]
        public void Should_Handle_AssignCourse_Command(string number, string name, int passMark)
        {
            object[] givenPriorEvents = new object[] { };
            var whenCommand = new AssignCourse(AggregateId, number, name, passMark);
            var expectedEvents = new object[] { new CourseAssigned(AggregateId, number, name, passMark) };
            IEnumerable<object> actualEvents = null;

            this.Given(_ => GivenPriorEvents(AggregateId, givenPriorEvents), false)
                .When(_ => WhenDispatchingCommand<AssignCourse>(whenCommand, out actualEvents), false)
                .Then(_ => ThenExpectedEventsAreGenerated(expectedEvents, actualEvents), false)
                .And(_ => ThenTheIdMatches())
                .And(_ => ThenTheCourseNumberMatches(number))
                .And(_ => ThenTheCourseNameMatches(name))
                .And(_ => ThenThePassMarkMatches(passMark))
                .BDDfy();
        }

        [Theory]
        [InlineData("PROG 1001", "Programming Fundamentals", 50)]
        [InlineData("COMP 2018", "Application Development", 70)]
        public void Should_Handle_CourseAssigned_Event(string number, string name, int passMark)
        {
            object[] givenPriorEvents = new object[] { new CourseAssigned(AggregateId, number, name, passMark) };
            //var expectedSut = new Course() { Number = number, Name = name, PassMark = passMark };

            this.Given(_ => GivenPriorEvents(AggregateId, givenPriorEvents), false)
                .Then(_ => ThenTheIdMatches())
                .And(_ => ThenTheCourseNumberMatches(number))
                .And(_ => ThenTheCourseNameMatches(name))
                .And(_ => ThenThePassMarkMatches(passMark))
                //.And(_ => ThenTheUncommittedEventCountIs(0)) // RESUME: here - about unsaved events
                .BDDfy();
        }
        private void ThenTheIdMatches()
        {
            Assert.Equal(AggregateId, SUT_ActualDomain.Id);
        }
        private void ThenTheCourseNumberMatches(string expected)
        {
            Assert.Equal(expected, SUT_ActualDomain.Number);
        }
        private void ThenTheCourseNameMatches(string expected)
        {
            Assert.Equal(expected, SUT_ActualDomain.Name);
        }
        private void ThenThePassMarkMatches(int expected)
        {
            Assert.Equal(expected, SUT_ActualDomain.PassMark);
        }
        private void ThenTheUncommittedEventCountIs(int count)
        {
            Assert.Equal(count, SUT_ActualDomain.EventsLoaded);
        }
        #endregion

        #region Alternate Scenarios for Domain
        [Theory]
        [InlineData("PROG 1001", "Programming Fundamentals", 50)]
        [InlineData("COMP 2018", "Application Development", 70)]
        public void Rejects_AssignCourse_With_Duplicate_Name(string number, string name, int passMark)
        {
            // TODO: Build exception environment
            object[] givenPriorEvents = new object[] { new CourseAssigned(AggregateId, number + "A", name, passMark) };
            var whenCommand = new AssignCourse(AggregateId, number, name, passMark);

            CourseDuplication actualEvents;
            this.Given(_ => GivenPriorEvents(AggregateId, givenPriorEvents), false)
                .When(_ => FailWhenDispatchingCommand<AssignCourse, CourseDuplication>(whenCommand, out actualEvents))
                .BDDfy();
        }

        [Theory]
        [InlineData("PROG 1001", "Programming Fundamentals", 50)]
        [InlineData("COMP 2018", "Application Development", 70)]
        public void Rejects_AssignCourse_With_Duplicate_Number(string number, string name, int passMark)
        {
            // TODO: Build exception environment
            object[] givenPriorEvents = new object[] { new CourseAssigned(AggregateId, number, name + " Alt", passMark) };
            var whenCommand = new AssignCourse(AggregateId, number, name, passMark);

            CourseDuplication actualEvents;
            this.Given(_ => GivenPriorEvents(AggregateId, givenPriorEvents), false)
                .When(_ => FailWhenDispatchingCommand<AssignCourse, CourseDuplication>(whenCommand, out actualEvents))
                .BDDfy();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void Rejects_AssignCourse_With_Invalid_Name(string name)
        {
            object[] givenPriorEvents = new object[] { };
            var whenCommand = new AssignCourse(AggregateId, "PROG 1001", name, 50);

            CourseNameInvalid actualEvents;
            this.Given(_ => GivenPriorEvents(AggregateId, givenPriorEvents), false)
                .When(_ => FailWhenDispatchingCommand<AssignCourse, CourseNameInvalid>(whenCommand, out actualEvents))
                .BDDfy();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void Rejects_AssignCourse_With_Invalid_Number(string number)
        {
            object[] givenPriorEvents = new object[] { };
            var whenCommand = new AssignCourse(AggregateId, number, "Programming Fundamentals", 50);

            CourseNumberInvalid actualEvents;
            this.Given(_ => GivenPriorEvents(AggregateId, givenPriorEvents), false)
                .When(_ => FailWhenDispatchingCommand<AssignCourse, CourseNumberInvalid>(whenCommand, out actualEvents))
                .BDDfy();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(49)]
        [InlineData(76)]
        [InlineData(100)]
        public void Rejects_AssignCourse_With_Invalid_PassMark(int passMark)
        {
            object[] givenPriorEvents = new object[] { };
            var whenCommand = new AssignCourse(AggregateId, "PROG 1001", "Programming Fundamentals", passMark);

            PassMarkIsInvalid actualEvents;
            this.Given(_ => GivenPriorEvents(AggregateId, givenPriorEvents), false)
                .When(_ => FailWhenDispatchingCommand<AssignCourse, PassMarkIsInvalid>(whenCommand, out actualEvents))
                .BDDfy();
        }
        #endregion
        #endregion

        #region ChangePassMark command & PassMarkChanged event
        #region Primary Domain Scenario
        [Theory]
        [InlineData("PROG 1001", "Programming Fundamentals", 50)]
        [InlineData("COMP 2018", "Application Development", 70)]
        public void Should_Handle_ChangePassMark(string number, string name, int passMark)
        {
            int newPassMark = 60;
            object[] givenPriorEvents = new object[] { new CourseAssigned(AggregateId, number, name, passMark) };
            var whenCommand = new ChangePassMark(AggregateId, newPassMark);
            var expectedEvents = new object[] { new PassMarkChanged { Id = AggregateId, PassMark = newPassMark } };
            IEnumerable<object> actualEvents = null;

            this.Given(_ => GivenPriorEvents(AggregateId, givenPriorEvents), false)
                .When(_ => WhenDispatchingCommand<ChangePassMark>(whenCommand, out actualEvents))
                .Then(_ => ThenExpectedEventsAreGenerated(expectedEvents, actualEvents))
                .And(_ => ThenTheIdMatches())
                .And(_ => ThenThePassMarkMatches(newPassMark))
                .BDDfy();
        }
        #endregion

        #region Alternate Scenarios for Domain
        [Theory]
        [InlineData(0)]
        [InlineData(49)]
        [InlineData(76)]
        [InlineData(100)]
        public void Rejects_ChangePassMark_With_Invalid_PassMark(int passMark)
        {
            object[] givenPriorEvents = new object[] { new CourseAssigned(AggregateId, "PROG 1001", "Programming Fundamentals", 60) };
            var whenCommand = new ChangePassMark(AggregateId, passMark);

            PassMarkIsInvalid actualEvents;
            this.Given(_ => GivenPriorEvents(AggregateId, givenPriorEvents), false)
                .When(_ => FailWhenDispatchingCommand<ChangePassMark, PassMarkIsInvalid>(whenCommand, out actualEvents))
                .BDDfy();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(49)]
        [InlineData(76)]
        [InlineData(100)]
        public void Rejects_ChangePassMark_With_Incorrect_Id(int passMark)
        {
            object[] givenPriorEvents = new object[] { new CourseAssigned(AggregateId, "PROG 1001", "Programming Fundamentals", 60) };
            var whenCommand = new ChangePassMark(Guid.NewGuid(), passMark);

            IdentityMismatch actualEvents;
            this.Given(_ => GivenPriorEvents(AggregateId, givenPriorEvents), false)
                .When(_ => FailWhenDispatchingCommand<ChangePassMark, IdentityMismatch>(whenCommand, out actualEvents))
                .BDDfy();
        }
        #endregion
        #endregion
    }
}
