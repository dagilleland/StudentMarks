using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edument.CQRS.Specs.Commands
{
    public class DoThat : ICommand
    {
        public Guid Id { get; set; }
        public string Task { get; set; }
        public DateTime DueDate { get; set; }
    }
}
