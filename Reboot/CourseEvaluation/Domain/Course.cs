using Edument.CQRS;
using StudentMarks.Framework.CourseEvaluation.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using StudentMarks.Framework.CourseEvaluation.Events;

namespace StudentMarks.Framework.CourseEvaluation.Domain
{
    public class Course : Aggregate, 
        IHandleCommand<AssignCourse>, IApplyEvent<CourseAssigned>,
        IHandleCommand<ChangePassMark>
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
            if (course.PassMark < 50 || course.PassMark > 75) throw new PassMarkIsInvalid();
            ApplyId(course.Id);

            // Note: yield return ... will result in a re-evaluation of the above ApplyId(), causing a run-time error/exception
            return new object[] { new CourseAssigned(course.Id, course.CourseNumber, course.CourseName, course.PassMark) };
        }

        public IEnumerable Handle(ChangePassMark c)
        {
            if (c.Id != Id) throw new IdentityMismatch();
            return new object[] { new PassMarkChanged(c.Id, c.PassMark) };
        }

        public void Apply(CourseAssigned eventData)
        {
            ApplyId(eventData.Id);
            Number = eventData.CourseNumber;
            Name = eventData.CourseName;
            PassMark = eventData.PassMark;
        }
    }
}
