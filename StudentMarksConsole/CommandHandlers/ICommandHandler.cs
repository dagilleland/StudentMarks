using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMarks.Framework.CommandHandlers
{
    public interface ICommandHandler<TCommand> where TCommand : class
    {
        void Execute(TCommand command);
    }
}
