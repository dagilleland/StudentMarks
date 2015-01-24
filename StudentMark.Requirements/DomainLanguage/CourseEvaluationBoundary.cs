using StudentMarks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;

namespace StudentMark.Requirements.DomainLanguage
{
    public class CourseOfferingBoundary
    {
    }
    public class CourseEvaluationBoundary
    {
        public class CourseEvaluationAggregate
        {
            public class TheAddMethod
            {
                [Fact]
                public void ShouldAddACompontent()
                {
                    throw new NotImplementedException();
                }
            }
        }

        public class EvaluationSet_ValueType
        {
            [Fact]
            public void IsValueTypeOfT()
            {
                var sut = new EvaluationSet("Valid Name", 10);
                Assert.True(sut is IEquatable<EvaluationSet>, "Should support IEquatable<T>");
            }

            [Theory]
            [InlineData("")]
            [InlineData(null)]
            public void ShouldRejectEmptyName(string name)
            {
                Assert.Throws<ArgumentException>(() => { var sut = new EvaluationSet(name, 10); });
            }

            [Theory]
            [InlineData(0)]
            [InlineData(-1)]
            [InlineData(101)]
            public void ShouldRejectInvalidWeight(int weight)
            {
                Assert.Throws<ArgumentException>(() => { var sut = new EvaluationSet("Valid Name", weight); });
            }
        }

        public class SubComponent_ValueType
        {
            [Fact]
            public void IsValueTypeOfT()
            {
                var sut = new SubComponent(null, "Valid Name", 10);
                Assert.True(sut is IEquatable<SubComponent>, "Should support IEquatable<T>");
            }

            [Fact]
            public void ShouldRejectNullParent(string name)
            {
                Assert.Throws<ArgumentException>(() => { var sut = new SubComponent(null, "Valid Name", 10); });
            }

            [Theory]
            [InlineData("")]
            [InlineData(null)]
            public void ShouldRejectEmptyName(string name)
            {
                Assert.Throws<ArgumentException>(() => { var sut = new SubComponent(null, name, 10); });
            }

            [Theory]
            [InlineData(0)]
            [InlineData(-1)]
            [InlineData(101)]
            public void ShouldRejectInvalidWeight(int weight)
            {
                Assert.Throws<ArgumentException>(() => { var sut = new SubComponent(null, "Valid Name", weight); });
            }

            [Fact]
            public void ShouldBeEqualAsValueObjects()
            {
                var first = new SubComponent(null, "Valid Name", 7);
                var second = new SubComponent(null, "Valid Name", 7);
                Assert.Equal(first, second);
            }
        }

        public class EvaluationComponent_ValueType
        {
            [Fact]
            public void IsValueTypeOfT()
            {
                var sut =  new EvaluationComponent("Valid Name", 10);
                Assert.True(sut is IEquatable<EvaluationComponent>, "Should support IEquatable<T>");
            }

            [Theory]
            [InlineData("")]
            [InlineData(null)]
            public void ShouldRejectEmptyName(string name)
            {
                Assert.Throws<ArgumentException>(() => { var sut = new EvaluationComponent(name, 10); });
            }

            [Theory]
            [InlineData(0)]
            [InlineData(-1)]
            [InlineData(101)]
            public void ShouldRejectInvalidWeight(int weight)
            {
                Assert.Throws<ArgumentException>(() => { var sut = new EvaluationComponent("Valid Name", weight); });
            }

            [Fact]
            public void ShouldBeEqualAsValueObjects()
            {
                var first = new EvaluationComponent("Valid Name", 7);
                var second = new EvaluationComponent("Valid Name", 7);
                Assert.Equal(first, second);
            }
        }
    }
}
