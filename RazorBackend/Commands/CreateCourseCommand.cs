using SimpleCqrs.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentMarks.Framework.Commands
{
    public class CreateCourseCommand : ICommand
    {
        public CreateCourseCommand()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCourseCommand"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="number"></param>
        /// <param name="pass"></param>
        public CreateCourseCommand(string name, string number, int pass)
        {
            Name = name;
            Number = number;
            Pass = pass;
        }
        public string Name { get; set; }
        public string Number { get; set; }
        public int Pass { get; set; }
    }
}
