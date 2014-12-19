using StudentMark.Requirements.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.BDDfy;
//using TestStack.BDDfy.Core;
//using TestStack.BDDfy.Scanners.StepScanners.Fluent;
using Xunit;
using Xunit.Extensions;
using StudentMarks.Models.Entities;
using StudentMarks.Controllers;
using System.Web.Http;
using System.Web.Http.Results;

namespace StudentMark.Requirements.UserStories
{
    [Story(AsA = Actor.INSTRUCTOR,
               IWant = "To enter student marks",
               SoThat = "I can track each student's progress in the course",
               Title = "Enter Student Marks")]
    public class EnterStudentMarks
    {
        #region Primary - Get Student Marks
        [Fact]
        //[AutoRollback]
        public void EnterClassMarks()
        {
            this.Given(_ => GivenAnExistingClassList())
                .And(_ => GivenAnExistingMarkStructure())
                .And(_ => GivenMarksHaveNotBeenEntered())
                .When(_ => WhenIEnterMarks())
                .Then(_ => ThenClassMarksCanBeRetrievedForViewing())
                .BDDfy();
        }
        private void GivenAnExistingClassList()
        {
            throw new NotImplementedException();
        }
        private Task GivenAnExistingMarkStructure()
        {
            throw new NotImplementedException();
        }
        private Task GivenMarksHaveNotBeenEntered()
        {
            throw new NotImplementedException();
        }
        private Task WhenIEnterMarks()
        {
            throw new NotImplementedException();
        }
        private Task ThenClassMarksCanBeRetrievedForViewing()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
