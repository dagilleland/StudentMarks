using System;

namespace StudentMarks.Framework.CourseEvaluation.Commands
{
    public abstract class AbstractCommand
    {
        public Guid Id { get; protected set; }

        public AbstractCommand(Guid id)
        {
            Id = id;
        }
    }
}
