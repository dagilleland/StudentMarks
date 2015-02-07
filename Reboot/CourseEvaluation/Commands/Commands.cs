using System;

namespace StudentMarks.Framework.CourseEvaluation.Commands
{
    public class AssignCourse
    {
        public string CourseNumber { get; private set; }
        public string CourseName { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="AssignCourse"/> class.
        /// </summary>
        /// <param name="courseNumber"></param>
        /// <param name="courseName"></param>
        public AssignCourse(string courseNumber, string courseName)
        {
            CourseNumber = courseNumber;
            CourseName = courseName;
        }
    }
    public class FixPassMark
    {
        public Guid Id { get; private set; }
        public int PassMark { get; private set; }
    }
    public class SetEvalutionComponents
    {
        public Guid Id { get; private set; }
    }
    public class MakeCourseAvailable
    {
        public Guid Id { get; private set; }
    }
    public class ReevaluateCourse
    {
        public Guid Id { get; private set; }
    }
    public class RetireCourse
    {
        public Guid Id { get; private set; }
    }
    public class ScrapCourse
    {
        public Guid Id { get; private set; }
    }
}
