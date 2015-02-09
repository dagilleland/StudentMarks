using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edument.CQRS.Specs.Commands
{
    public class GoThere : ICommand
    {
        public Guid Id { get; set; }
        public string Location { get; set; }
    }
}
