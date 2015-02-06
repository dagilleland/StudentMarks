using System;
using System.Collections.Generic;

namespace StudentMarks.Framework.CourseEvaluation.Events
{
    public class CourseAssigned
    {
        public string CourseNumber { get; private set; }
        public string CourseName { get; private set; }
    }
    public class PassMarkFixed
    {
        public Guid Id { get; private set; }
        public int PassMark { get; private set; }
    }
    public class EvaluationComponentsSet
    {
        public Guid Id { get; private set; }
    }
    public class CourseMadeAvailable
    {
        public Guid Id { get; private set; }
    }
    public class CourseReevaluated
    {
        public Guid Id { get; private set; }
    }
    public class CourseRetired
    {
        public Guid Id { get; private set; }
    }
    public class CourseScrapped
    {
        public Guid Id { get; private set; }
    }
}
