using NTestDataBuilder;
using StudentMarks.Models.Entities;
using StudentMarks.Models.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMark.Requirements.TestData
{

    public static partial class ObjectMother
    {
        // CREDITS: http://robdmoore.id.au/blog/2013/05/26/test-data-generation-the-right-way-object-mother-test-data-builders-nsubstitute-nbuilder/
        // CREDITS: http://martinfowler.com/bliki/ObjectMother.html
    }
    public class EvaluationComponentBuilder : TestDataBuilder<EvaluationComponent, EvaluationComponentBuilder>
    {

        protected override EvaluationComponent BuildObject()
        {
            throw new NotImplementedException();
        }
    }
    public class StudentBuilder : TestDataBuilder<Student, StudentBuilder>
    {
        public StudentBuilder()
        {
            WithFirstName("Stewart");
            WithLastName("Dent");
            WithSchoolId(1234567);
        }

        public StudentBuilder WithFirstName(string first)
        {
            Set(x => x.FirstName, first);
            return this;
        }

        public StudentBuilder WithLastName(string last)
        {
            Set(x => x.LastName, last);
            return this;
        }

        public StudentBuilder WithSchoolId(long schoolId)
        {
            Set(x => x.SchoolID, schoolId);
            return this;
        }

        protected override Student BuildObject()
        {
            return new Student()
            {
                FirstName = Get(x => x.FirstName),
                LastName = Get(x => x.LastName),
                SchoolID = Get(x => x.SchoolID)
            };
        }
    }
}
