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
           IWant = "To enter student names",
           SoThat = "I can manage my class list",
           Title = "Manage Class List")]
    public class ManageClassLists
    {
        [Theory]
        [AutoRollback]
        [InlineData("Auresh", "Nsand", 12331414408L)]
        [InlineData("Aonathan", "Rjca", 12307165510L)]
        [InlineData("Bicholas", "Rnownlee", 12331783816L)]
        [InlineData("Bindsay", "Ulrchill", 12331935204L)]
        [InlineData("Duchen", "Eyng", 12332227102L)]
        [InlineData("Gobert", "Rrell", 12331035102L)]
        [InlineData("Jcott", "Asmes", 12331518404L)]
        [InlineData("Kohamed", "Ammal", 12323049816L)]
        [InlineData("Kavjot", "Anur", 12330837102L)]
        [InlineData("Kmir", "Haiabani", 12331568408L)]
        [InlineData("Kkkyung", "Iom", 12330280918L)]
        [InlineData("Kushil", "Usmar", 12330282306L)]
        [InlineData("Llifford", "Acbelle", 12308729612L)]
        [InlineData("Mhawn", "Asckay", 12326831408L)]
        [InlineData("Magandeep", "Agnj", 12331647714L)]
        [InlineData("Mrandon", "Ebrinsky", 12332510306L)]
        [InlineData("Meban", "Olhamed", 12328453306L)]
        [InlineData("Mbdalla", "Uasa", 12326457000L)]
        [InlineData("Pshima", "Aal", 12330524816L)]
        [InlineData("Rurray", "Imce", 975543408L)]
        [InlineData("Saley", "Ohmmer", 12330531612L)]
        [InlineData("Samel", "Tjevenson", 12316467000L)]
        [InlineData("Sustin", "Wdieringa", 12331937714L)]
        [InlineData("Wessica", "Ijkstrom", 12331121714L)]
        [InlineData("Zidan", "Hyao", 12331571000L)]
        public void EnterStudentNames(string first, string last, long schoolId)
        {
            Student expected = new Student(), actual = null;
            this.Given(_ => GivenRawStudentData(first, last, schoolId, expected))
                .When(_ => WhenIAddAStudent(expected, out actual))
                .Then(_ => ThenTheStudentHasBeenAdded(expected, actual))
                .BDDfy();
        }
        private void GivenRawStudentData(string first, string last, long schoolId, Student obj)
        {
            obj.FirstName = first;
            obj.LastName = last;
            obj.SchoolID = schoolId;
        }

        private void WhenIAddAStudent(Student expected, out Student actual)
        {
            StudentsController sut = new StudentsController();
            // IHttpActionResult result = sut.PostStudent(expected);

            var contentResult = sut.PostStudent(expected); // result as CreatedAtRouteNegotiatedContentResult<Student>;
            Assert.NotNull(contentResult);
            //Assert.NotNull(contentResult.Content);
            actual = contentResult; //.Content;
        }

        private void ThenTheStudentHasBeenAdded(Student expected, Student actual)
        {
            StudentsController sut = new StudentsController();
            actual = (sut.GetStudent(actual.Id)) ;// as OkNegotiatedContentResult<Student>).Content;
            Assert.Equal(expected.FirstName, actual.FirstName);
            Assert.Equal(expected.LastName, actual.LastName);
            Assert.Equal(expected.SchoolID, actual.SchoolID);
        }
    }
}

