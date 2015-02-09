using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edument.CQRS.Specs.Commands
{
    /// <summary>
    /// Commands represent requests to the application domain.
    /// Commands are "outward facing", and may be accepted or rejected
    /// by the domain according to the business rules of the domain.
    /// </summary>
    public interface ICommand
    {
    }
}
