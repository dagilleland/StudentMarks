using StudentMarks.Framework.Events;
using StudentMarks.Framework.EventStores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMarks.Framework.Domains.CourseEnrollment
{
    public class Course : BaseAggregateRoot<IDomainEvent>
    {
        private CourseNumber _courseNumber;
        private CourseName _courseName;

        public Course()
        {
            registerEvents();
        }

        private Course(CourseNumber courseNumber, CourseName courseName)
            : this()
        {
            Apply(new CourseCreatedEvent(Guid.NewGuid(), courseNumber.Number, courseName.Name));
        }

        public static Course CreateNew(CourseNumber courseNumber, CourseName courseName)
        {
            return new Course(courseNumber, courseName);
        }
        private void registerEvents()
        {
            RegisterEvent<CourseCreatedEvent>(onNewCourseCreated);
        }

        private void onNewCourseCreated(CourseCreatedEvent courseCreatedEvent)
        {
            Id = courseCreatedEvent.Id;
            _courseNumber = new CourseNumber(courseCreatedEvent.CourseNumber);
            _courseName = new CourseName(courseCreatedEvent.CourseName);
        }
    }

    public class CourseNumber
    {
        public string Number { get; private set; }
        public CourseNumber(string number)
        {
            Number = number;
        }
    }

    public class CourseName
    {
        public string Name { get; private set; }
        public CourseName(string name)
        {
            Name = name;
        }
    }
}
