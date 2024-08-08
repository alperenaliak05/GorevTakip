using Microsoft.EntityFrameworkCore;
using TaskApp_Web.Models;

namespace TaskApp_Web.Data
{
    public class TaskApp_WebContext : DbContext
    {
        public TaskApp_WebContext(DbContextOptions<TaskApp_WebContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<ToDoTask> Tasks { get; set; }
        public DbSet<Departments> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<ToDoTask>()
                .HasOne(t => t.AssignedToUser)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.AssignedToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ToDoTask>()
                .HasOne(t => t.AssignedByUser)
                .WithMany()
                .HasForeignKey(t => t.AssignedByUserId)
                .OnDelete(DeleteBehavior.Restrict); 

            base.OnModelCreating(modelBuilder);
        }
    }
}
