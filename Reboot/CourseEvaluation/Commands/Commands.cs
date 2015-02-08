using StudentMarks.Framework.CourseEvaluation.Domain;
using System;

namespace StudentMarks.Framework.CourseEvaluation.Commands
{
    public class AssignCourse : AbstractCommand
    {
        public string CourseNumber { get; private set; }
        public string CourseName { get; private set; }
        public int PassMark { get; private set; }

        public AssignCourse(Guid aggregateId, string courseNumber, string courseName, int passMark)
            : base(aggregateId)
        {
            CourseNumber = courseNumber;
            CourseName = courseName;
            PassMark = passMark;
        }
    }
    public class ChangePassMark : AbstractCommand
    {
        public int PassMark { get; private set; }
        public ChangePassMark(Guid aggregateId, int passMark)
            : base(aggregateId)
        {
            PassMark = passMark;
        }
    }
    public class SetEvalutionComponents : AbstractCommand
    {
        public SetEvalutionComponents(Guid aggregateId)
            : base(aggregateId)
        {
        }
    }
    public class MakeCourseAvailable : AbstractCommand
    {
        public MakeCourseAvailable(Guid aggregateId)
            : base(aggregateId)
        {
        }
    }
    public class ReevaluateCourse : AbstractCommand
    {
        public ReevaluateCourse(Guid aggregateId)
            : base(aggregateId)
        {
        }
    }
    public class RetireCourse : AbstractCommand
    {
        public RetireCourse(Guid aggregateId)
            : base(aggregateId)
        {
        }
    }
    public class ScrapCourse : AbstractCommand
    {
        public ScrapCourse(Guid aggregateId)
            : base(aggregateId)
        {
        }
    }
}
