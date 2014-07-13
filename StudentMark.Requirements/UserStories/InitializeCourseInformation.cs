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
using StudentMarks.App.DAL;

namespace StudentMark.Requirements.UserStories
{
    [Story(AsA = Actor.INSTRUCTOR,
               IWant = "To enter the mark structure for the course",
               SoThat = "I have the basic framework to enter student marks")]
    public class InitializeCourseInformation
    {
        #region Primary - Enter Evaluation Components
        IList<Topic> ExpectedTopics;
        IList<Quiz> ExpectedQuizzes;
        IList<Bucket> ExpectedBuckets;
        CourseConfiguration ExpectedConfiguration;

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
            CourseConfigController sut = new CourseConfigController(AppContext.Create());
            Assert.Equal(0, sut.GetBucketTopics().Count);
            Assert.Equal(0, sut.GetQuizzes().Count);
            Assert.Equal(0, sut.GetBuckets().Count);
            sut.Dispose();
        }
        private void GivenEvaluationComponentDataToBeEntered()
        {
            ExpectedTopics = new List<Topic>();
            ExpectedTopics.Add(new Topic() { Description = "Network Model" });
            ExpectedTopics.Add(new Topic() { Description = "IP Addressing" });
            ExpectedTopics.Add(new Topic() { Description = "Virtual Machines" });

            ExpectedQuizzes = new List<Quiz>();
            ExpectedQuizzes.Add(new Quiz() { Name = "Quiz A", Weight = 25, PotentialMarks = 40 });
            ExpectedQuizzes.Add(new Quiz() { Name = "Quiz B", Weight = 15 });
            ExpectedQuizzes.Add(new Quiz() { Name = "Quiz C", Weight = 10 });

            ExpectedConfiguration = new CourseConfiguration() { BucketWeight = 10 };

            ExpectedBuckets = new List<Bucket>();
            ExpectedBuckets.Add(new Bucket() { Name = "OSI Model", Topic = ExpectedTopics[0], Weight = ExpectedConfiguration.BucketWeight });
            ExpectedBuckets.Add(new Bucket() { Name = "Network Diagramming", Topic = ExpectedTopics[0], Weight = ExpectedConfiguration.BucketWeight });
            ExpectedBuckets.Add(new Bucket() { Name = "DCHP and IPConfig", Topic = ExpectedTopics[1], Weight = ExpectedConfiguration.BucketWeight });
            ExpectedBuckets.Add(new Bucket() { Name = "Classifying IP Addresses", Topic = ExpectedTopics[1], Weight = ExpectedConfiguration.BucketWeight });
            ExpectedBuckets.Add(new Bucket() { Name = "Backup to a Virtual Machine", Topic = ExpectedTopics[2], Weight = ExpectedConfiguration.BucketWeight });
        }
        private void WhenIAddEvaluationComponents()
        {
            CourseConfigController sut = new CourseConfigController(AppContext.Create());
            sut.AddCourseConfiguration(ExpectedConfiguration);
            sut.AddBuckets(ExpectedBuckets);
            sut.AddQuizzes(ExpectedQuizzes);
            sut.Dispose();
        }
        private void ThenICanRetrieveEvaluationComponentsForTheCourse()
        {
            CourseConfigController sut = new CourseConfigController(AppContext.Create());
            Assert.Equal(ExpectedTopics.Count, sut.GetBucketTopics().Count);
            Assert.Equal(ExpectedQuizzes.Count, sut.GetQuizzes().Count);
            Assert.Equal(ExpectedBuckets.Count, sut.GetBuckets().Count);
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
