using System;
using System.Collections.Generic;

namespace StudentMarks.Framework.CourseEvaluation.Events
{
    [Serializable]
    public class CourseAssigned
    {
        public string CourseNumber { get;  set; }
        public string CourseName { get; set; }
        public int PassMark { get; set; }
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
        public CourseAssigned() { }
    }
    [Serializable]
    public class PassMarkFixed
    {
        public Guid Id { get; set; }
        public int PassMark { get; set; }
    }
    public class EvaluationComponentsSet
    {
        public Guid Id { get; set; }
    }
    public class CourseMadeAvailable
    {
        public Guid Id { get; set; }
    }
    public class CourseReevaluated
    {
        public Guid Id { get; set; }
    }
    public class CourseRetired
    {
        public Guid Id { get; set; }
    }
    public class CourseScrapped
    {
        public Guid Id { get; set; }
    }
}
