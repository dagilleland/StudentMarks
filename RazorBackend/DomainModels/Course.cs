using SimpleCqrs.Domain;
using StudentMarks.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMarks.Framework.DomainModels
{
    public class Course : AggregateRoot
    {
        public Course(Guid id, string number, string name, int pass)
        {
            Apply(new CourseCreatedEvent() { AggregateRootId = id });
        }
    }
}
