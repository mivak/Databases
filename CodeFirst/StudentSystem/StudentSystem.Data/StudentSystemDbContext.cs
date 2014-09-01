namespace StudentSystem.Data
{
    using System.Data.Entity;

    using StudentSystem.Model;
    using StudentSystem.Data.Migrations;
    
    public class StudentSystemDbContext : DbContext
    {
        public StudentSystemDbContext()// : base("StudentSystem")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentSystemDbContext, Configuration>());
        }

        public IDbSet<Student> Students { get; set; }

        public IDbSet<Course> Courses { get; set; }

        public IDbSet<Homework> Homeworks { get; set; }
    }
}