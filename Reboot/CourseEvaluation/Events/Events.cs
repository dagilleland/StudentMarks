using System;
using System.Collections.Generic;

namespace StudentMarks.Framework.CourseEvaluation.Events
{
    public class CourseAssigned
    {
        internal string CourseNumber { get;  set; }
        internal string CourseName { get; set; }
        internal int PassMark { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="CourseAssigned"/> class.
        /// </summary>
        /// <param name="aggregateId"></param>
        /// <param name="courseNumber"></param>
        /// <param name="courseName"></param>
        /// <param name="passMark"></param>
        public CourseAssigned(Guid aggregateId, string courseNumber, string courseName, int passMark)
        {
            CourseNumber = courseNumber;
            CourseName = courseName;
        }
        internal CourseAssigned() { }
    }
    public class PassMarkFixed
    {
        internal Guid Id { get; set; }
        internal int PassMark { get; set; }
    }
    public class EvaluationComponentsSet
    {
        internal Guid Id { get; set; }
    }
    public class CourseMadeAvailable
    {
        internal Guid Id { get; set; }
    }
    public class CourseReevaluated
    {
        internal Guid Id { get; set; }
    }
    public class CourseRetired
    {
        internal Guid Id { get; set; }
    }
    public class CourseScrapped
    {
        internal Guid Id { get; set; }
    }
}
