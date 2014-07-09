using StudentMark.Requirements.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.BDDfy;
using TestStack.BDDfy.Core;
using TestStack.BDDfy.Scanners.StepScanners.Fluent;
using Xunit;
using Xunit.Extensions;
using StudentMarks.Models.Entities;
using StudentMarks.Controllers;
using System.Web.Http;
using System.Web.Http.Results;

namespace StudentMark.Requirements.UserStories
{
    [Story(AsA = Actor.INSTRUCTOR,
               IWant = "To enter the mark structure for the course",
               SoThat = "I have the basic framework to enter student marks")]
    public class InitializeCourseInformation
    {
        #region Primary - Enter Evaluation Components
        [Fact]
        [AutoRollback]
        public void EnterEvaluationComponents()
        {
            this.Given(_ => GivenNoEvaluationComponentInformationHasBeenEntered())
                .And(_ => GivenEvaluationComponentDataToBeEntered())
                .When(_ => WhenIAddEvaluationComponents())
                .Then(_ => ThenICanRetrieveEvaluationComponentsForTheCourse())
                .BDDfy();
        }
        private void GivenNoEvaluationComponentInformationHasBeenEntered()
        {
            throw new NotImplementedException();
        }
        private Task GivenEvaluationComponentDataToBeEntered()
        {
            throw new NotImplementedException();
        }
        private Task WhenIAddEvaluationComponents()
        {
            throw new NotImplementedException();
        }
        private Task ThenICanRetrieveEvaluationComponentsForTheCourse()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Primary - Enter Course Name
        [Fact]
        [AutoRollback]
        public void SetCourseName()
        {
            this.When(_ => WhenIEnterACourseName("Networking 101"))
                .Then(_ => ThenTheCourseNameIsRecorded("Networking 101"))
                .BDDfy();
        }
        private void WhenIEnterACourseName(string name)
        {
            CourseConfigController app = new CourseConfigController();
            app.SetCourseName(name);
        }
        private void ThenTheCourseNameIsRecorded(string expectedName)
        {
            CourseConfigController app = new CourseConfigController();
            var actual = app.GetCourseName();
            Assert.Equal(expectedName, actual);
        }
        #endregion
    }
}
