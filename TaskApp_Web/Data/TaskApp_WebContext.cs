using Microsoft.EntityFrameworkCore;
using TaskApp_Web.Models;

namespace TaskApp_Web.Data
{
    public class TaskApp_WebContext : DbContext
    {
        public TaskApp_WebContext(DbContextOptions<TaskApp_WebContext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<ToDoTasks> Tasks { get; set; }
        public DbSet<UserToDoList> UserToDoLists { get; set; }
        public DbSet<TaskReport> TaskReports { get; set; }
        public DbSet<TaskProcess> TaskProcesses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Users ve Departments arasındaki ilişki
            modelBuilder.Entity<Users>()
                .HasOne(u => u.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict); // Silme işlemi kısıtlaması

            // ToDoTasks ve Users (AssignedToUser) arasındaki ilişki
            modelBuilder.Entity<ToDoTasks>()
                .HasOne(t => t.AssignedToUser)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.AssignedToUserId)
                .OnDelete(DeleteBehavior.Restrict); // Silme işlemi kısıtlaması

            // ToDoTasks ve Users (AssignedByUser) arasındaki ilişki
            modelBuilder.Entity<ToDoTasks>()
                .HasOne(t => t.AssignedByUser)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssignedByUserId)
                .OnDelete(DeleteBehavior.Restrict); // Silme işlemi kısıtlaması

            // ToDoTasks Status alanı enum conversion
            modelBuilder.Entity<ToDoTasks>()
               .Property(t => t.Status)
               .HasConversion<int>();

            // UserToDoList ve Users arasındaki ilişki
            modelBuilder.Entity<UserToDoList>()
                .HasOne(t => t.User)
                .WithMany(u => u.ToDoLists)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Kullanıcı silindiğinde ilgili todo listelerini sil

            // TaskReport ve ToDoTasks arasındaki ilişki
            modelBuilder.Entity<TaskReport>()
               .HasOne(tr => tr.Task)
               .WithMany(t => t.TaskReports)
               .HasForeignKey(tr => tr.TaskId)
               .OnDelete(DeleteBehavior.Cascade); // Görev silindiğinde ilgili raporları da sil

            // TaskReport ve Users (CreatedByUser) arasındaki ilişki
            modelBuilder.Entity<TaskReport>()
                .HasOne(tr => tr.CreatedByUser)
                .WithMany(u => u.TaskReports)
                .HasForeignKey(tr => tr.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict); // Silme işlemi kısıtlaması

            modelBuilder.Entity<TaskProcess>()
             .HasOne(tp => tp.Task)  // TaskProcess, ToDoTasks ile ilişkilendirilir
             .WithMany(t => t.TaskProcesses)  // ToDoTasks, TaskProcess ilişkisine sahiptir
             .HasForeignKey(tp => tp.TaskId)  // TaskProcess üzerindeki foreign key
             .OnDelete(DeleteBehavior.Cascade); // 
        }
    }
}
