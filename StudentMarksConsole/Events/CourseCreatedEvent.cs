using StudentMarks.Framework.EventStores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMarks.Framework.Events
{
    public class CourseCreatedEvent : DomainEvent
    {
        public Guid CourseId { get; private set; }
        public string CourseNumber { get; private set; }
        public string CourseName { get; private set; }

        public CourseCreatedEvent(Guid courseId, string courseNumber, string courseName)
        {
            CourseId = courseId;
            CourseNumber = courseNumber;
            CourseName = courseName;
        }
    }

    [Serializable]
    public class DomainEvent : IDomainEvent
    {
        public Guid Id { get; private set; }
        public Guid AggregateId { get; set; }
        int IDomainEvent.Version { get; set; }

        public DomainEvent()
        {
            Id = Guid.NewGuid();
        }
    }
}
