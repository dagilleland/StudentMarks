using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;

namespace StudentMarks.Framework.UserStories.Course_Evaluation_BoundedContext
{
    [Story(AsA="Instructor",
           IWant="",
           SoThat="")]
    public class Adding_A_Course
    {
        [Fact]
        public void Test()
        {

            this.When(_ => When_I_Create_A_Course(Guid.NewGuid(), "COMP101", "Intro to C#"))
                .BDDfy();
        }
        private void When_I_Create_A_Course(Guid id, string courseNumber, string courseName)
        {
            
        }
    }

    public class Set_Course_Evaluation_Components
    {
        
    }
}

namespace StudentMarks.Framework.UserStories.Course_Offering_BoundedContext
{
    public class ConfigureCourseEvaluations
    {
    }
}

namespace StudentMarks.Framework.UserStories.Course_Enrollment_BoundedContext
{
    public class ConfigureCourseEvaluations
    {
    }
}

namespace StudentMarks.Framework.UserStories.Student_Grading_BoundedContext
{
    public class ConfigureCourseEvaluations
    {
    }
}

namespace StudentMarks.Framework.UserStories.Security_BoundedContext
{
    public class TBA
    {
    }
}
