using Edument.CQRS;
using StudentMarks.Framework.CourseEvaluation.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using StudentMarks.Framework.CourseEvaluation.Events;

namespace StudentMarks.Framework.CourseEvaluation.Domain
{
    public class Course : Aggregate, IHandleCommand<AssignCourse>
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public int PassMark { get; set; }

        //public Course()
        //{
        //}
        //public Course(string number, string name)
        //{
        //    if (string.IsNullOrWhiteSpace(number))
        //        throw new ArgumentException("A number is required for a course");
        //    if (string.IsNullOrWhiteSpace(name))
        //        throw new ArgumentException("A name is required for a course");
        //    Number = number;
        //    Name = name;

        //}

        public IEnumerable Handle(AssignCourse course)
        {
            if (string.IsNullOrWhiteSpace(course.CourseName)) throw new CourseNameInvalid();
            if (string.IsNullOrWhiteSpace(course.CourseNumber)) throw new CourseNumberInvalid();

            yield return new CourseAssigned(Guid.Empty, course.CourseNumber, course.CourseName, 0);
        }
    }
}
