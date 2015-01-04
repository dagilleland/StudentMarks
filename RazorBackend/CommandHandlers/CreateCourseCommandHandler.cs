using StudentMarks.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleCqrs.Commanding;
using StudentMarks.Framework.DomainModels;
using SimpleCqrs.Domain;

namespace StudentMarks.Framework.CommandHandlers
{
    public class CreateCourseCommandHandler : CommandHandler<CreateCourseCommand>
    {
        private readonly IDomainRepository _domainRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCourseCommandHandler"/> class.
        /// </summary>
        public CreateCourseCommandHandler(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }
        public override void Handle(CreateCourseCommand command)
        {
            var course = new Course(Guid.NewGuid(), command.Number, command.Name, command.Pass);
            _domainRepository.Save(course);
        }
    }
}
