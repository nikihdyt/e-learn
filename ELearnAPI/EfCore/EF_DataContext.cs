using Microsoft.EntityFrameworkCore;

namespace ELearnAPI.EfCore
{
    public class EF_DataContext : DbContext
    {
        public EF_DataContext(DbContextOptions<EF_DataContext> options) : base(options) { }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseMaterial> CoursesMaterial { get; set;}
        public DbSet<Assignment> Assignments { get; set; }
    }
}
