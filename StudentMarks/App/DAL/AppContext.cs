using StudentMarks.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentMarks.App.DAL
{
    public class AppContext : DbContext
    {
        public AppContext()
            : base("DefaultConnection")
        {
        }

        public static AppContext Create()
        {
            return new AppContext();
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Bucket> Buckets { get; set; }
        public DbSet<CourseConfiguration> CourseCongiruations { get; set; }
    }
}