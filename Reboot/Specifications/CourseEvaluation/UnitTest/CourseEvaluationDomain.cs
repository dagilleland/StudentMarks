using StudentMarks.Framework.CourseEvaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;

namespace StudentMarks.Framework.Specifications.CourseEvaluation.UnitTest
{
    public class CourseEvaluationDomain
    {
        /* In the Course Evaluation bounded context, the domain
         *
         * A_Course
         *  Should_Require_A_Name
         *  Should_Require_A_Number
         *  Should_Reject_Duplicate_Name
         *  Should_Reject_Duplicate_Number
         *  Should_Set_Evaluation_Components
         *  Should_Be_Deliverable_If_Components_Total_100
         *
         *      0 < Weight <= 100
         *
         */

        public class Course_Should
        {
            [Theory]
            [InlineData("Programming Fundamentals")]
            public void Require_A_Name(string name)
            {
                var sut = new Course("COMP101", name);
                Assert.Equal(name, sut.Name);
            }

            [Theory]
            [InlineData("")]
            [InlineData(null)]
            [InlineData("\t")]
            [InlineData("\n")]
            [InlineData(" ")]
            public void Reject_Invalid_number(string number)
            {
                Assert.Throws<ArgumentException>(() => new Course("COMP101", number));
            }

            [Theory]
            [InlineData("Programming Fundamentals")]
            public void Require_A_Number(string number)
            {
                var sut = new Course(number, "Programming Fundamentals");
                Assert.Equal(number, sut.Number);
            }

            [Theory]
            [InlineData("")]
            [InlineData(null)]
            [InlineData("\t")]
            [InlineData("\n")]
            [InlineData(" ")]
            public void Reject_Invalid_Number(string number)
            {
                Assert.Throws<ArgumentException>(() => new Course(number, "Programming Fundamentals"));
            }
        }

        public class Evaluation_Component_Should
        {
            [Theory]
            [InlineData("Quiz 1")]
            [InlineData("Lab 1")]
            public void Require_A_Name(string name)
            {
                var sut = new EvaluationComponent(name, 10);
                Assert.Equal(name, sut.Name);
            }

            [Theory]
            [InlineData("")]
            [InlineData(null)]
            [InlineData("\t")]
            [InlineData("\n")]
            [InlineData(" ")]
            public void Reject_Invalid_Name(string name)
            {
                Assert.Throws<ArgumentException>(() => new EvaluationComponent(name, 10));
            }

            [Theory]
            [InlineData(1)]
            [InlineData(100)]
            [InlineData(77)]
            public void Require_A_Weight(int weight)
            {
                var sut = new EvaluationComponent("Quiz 1", weight);
                Assert.Equal(weight, sut.Weight);
            }

            [Theory]
            [InlineData(0)]
            [InlineData(101)]
            [InlineData(-1)]
            public void Reject_Invalid_Weight(int weight)
            {
                Assert.Throws<ArgumentException>(() => new EvaluationComponent("Quiz 1", weight));
            }
        }
    }
}
