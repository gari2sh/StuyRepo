using Microsoft.EntityFrameworkCore;
using StuyvesantHighSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuyvesantHighSchool.Data
{
    public class StuyDbContext : DbContext
    {
        public StuyDbContext(DbContextOptions<StuyDbContext> options) : base(options)
        {
        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<RoomAssignment> RoomAssignments { get; set; }
        public DbSet<SubjectAssignment> SubjectAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>().ToTable("Subject");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Teacher>().ToTable("Teacher");
            modelBuilder.Entity<RoomAssignment>().ToTable("RoomAssignment");
            modelBuilder.Entity<SubjectAssignment>().ToTable("SubjectAssignment");

            modelBuilder.Entity<SubjectAssignment>()
                .HasKey(c => new { c.SubjectID, c.TeacherID });
        }
    }
}
