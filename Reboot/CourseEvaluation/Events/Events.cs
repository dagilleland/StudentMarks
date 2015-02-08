using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace StudentMarks.Framework.CourseEvaluation.Events
{
    [Serializable]
    public class CourseAssigned
    {
        public Guid Id { get; set; }
        public string CourseNumber { get; set; }
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
            Id = aggregateId;
            CourseNumber = courseNumber;
            CourseName = courseName;
            PassMark = passMark;
        }
        public CourseAssigned() { }
    }
    [Serializable]
    public class PassMarkChanged
    {
        public Guid Id { get; set; }
        public int PassMark { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PassMarkChanged"/> class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="passMark"></param>
        public PassMarkChanged(Guid id, int passMark)
        {
            Id = id;
            PassMark = passMark;
        }

        public PassMarkChanged() { }
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
