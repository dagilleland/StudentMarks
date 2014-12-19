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
           IWant = "To view marks for the whole class",
           SoThat = "I can see the overall progress of my students",
           Title = "View Class Marks")]
    public class ViewClassMarks
    {
        #region Primary - Get Student Marks
        [Fact]
        //[AutoRollback]
        public void GetClassMarks()
        {
            this.Given(_ => GivenAnExistingClassList())
                .And(_ => GivenAnExistingMarkStructure())
                .And(_ => GivenMarksHaveBeenEntered())
                .When(_ => WhenIRequestMarks())
                .Then(_ => ThenClassMarksAreRetrievedForViewing())
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
        private Task GivenMarksHaveBeenEntered()
        {
            throw new NotImplementedException();
        }
        private Task WhenIRequestMarks()
        {
            throw new NotImplementedException();
        }
        private Task ThenClassMarksAreRetrievedForViewing()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
