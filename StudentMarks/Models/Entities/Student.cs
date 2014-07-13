using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentMarks.Models.Entities
{
    public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long SchoolID { get; set; }
    }


    public class Course
    {
        public int CourseID { get; set; }
        public string Name { get; set; }
    }
    public class CourseConfiguration
    {
        public int CourseConfigurationID { get; set; }

        public int BucketWeight { get; set; }
    }
    public class MarkableItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }

        public int DisplayOrder { get; set; }
    }
    public class Quiz : MarkableItem
    {
        public int PotentialMarks { get; set; }
    }
    public class Bucket : MarkableItem
    {
        public int TopicID { get; set; }
        public Topic Topic { get; set; }
    }
    public class Topic
    {
        public int TopicID { get; set; }
        public string Description { get; set; }


    }
}