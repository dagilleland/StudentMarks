using SimpleCqrs.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMarks.Framework.Commands
{
    public class AddCourseCommand : ICommand
    {
        public string CourseNumber { get; set; }
        public string CourseName { get; set; }
        public int PassMark { get; set; }
    }
}
