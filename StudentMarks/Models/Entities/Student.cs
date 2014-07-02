using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentMarks.Models.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long SchoolID { get; set; }
    }
}