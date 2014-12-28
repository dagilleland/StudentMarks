namespace StudentMarksMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class CourseGradesContext : DbContext
    {
        // Your context has been configured to use a 'CourseGradesContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'StudentMarksMVC.Models.CourseGradesContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CourseGradesContext' 
        // connection string in the application configuration file.
        public CourseGradesContext()
            : base("name=CourseGradesContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<EvaluationComponent> EvaluationComponents { get; set; }
        public virtual DbSet<SubComponent> SubComponents { get; set; }
        public virtual DbSet<OfferingEvaluation> OfferingEvaluations { get; set; }
        public virtual DbSet<CourseOffering> CourseOfferings { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<MarkedItem> MarkedItems { get; set; }
        public virtual DbSet<BucketItem> BucketItems { get; set; }
    }

    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }

        public ICollection<CourseOffering> CourseOfferings { get; set; }
        public ICollection<EvaluationComponent> EvaluationComponents { get; set; }
    }

    public class EvaluationComponent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Weight { get; set; }
        public bool IsControlled { get; set; }
        public bool IsArchived { get; set; }

        public ICollection<SubComponent> SubComponents { get; set; }
    }

    public class SubComponent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? Weight { get; set; }
        public bool IsBonus { get; set; }
        public bool IsPassFail { get; set; }
    }

    public class OfferingEvaluation
    {
        public int Id { get; set; }
        public EvaluationComponent EvaluationComponent { get; set; }
        public CourseOffering CourseOffering { get; set; }
    }

    public class CourseOffering
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<Section> Sections { get; set; }
        public ICollection<OfferingEvaluation> OfferingEvaluations { get; set; }
    }

    public class Section
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string InstructorName { get; set; }
        public ICollection<Member> Members { get; set; }
    }

    public class Member
    {
        public int Id { get; set; }
        public Section Section { get; set; }
        public Student Student { get; set; }
    }

    public class Student
    {
        public int Id { get; set; }
        public string SchoolId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PerferredName { get; set; }

        public ICollection<MarkedItem> MarkedItems { get; set; }
        public ICollection<BucketItem> BucketItems { get; set; }
    }

    public class MarkedItem
    {
        public int Id { get; set; }
        public int PossibleMarks { get; set; }
        public int EarnedMark { get; set; }
        public int? AssignedMark { get; set; }
        public string Comment { get; set; }

        public SubComponent SubComponent { get; set; }
        public EvaluationComponent EvaluationComponent { get; set; }
        public Student Student { get; set; }
    }

    public class BucketItem
    {
        public int Id { get; set; }
        public bool Pass { get; set; }
        public SubComponent SubComponent { get; set; }
        public Student Student { get; set; }
    }
}