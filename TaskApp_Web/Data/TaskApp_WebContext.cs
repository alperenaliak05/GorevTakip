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
        public DbSet<Badge> Badges { get; set; }
        public DbSet<UserBadge> UserBadges { get; set; }
        public DbSet<Information> Informations { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ToDoTasks>()
                .HasOne(t => t.AssignedToUser)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.AssignedToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ToDoTasks>()
                .HasOne(t => t.AssignedByUser)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssignedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ToDoTasks>()
               .Property(t => t.Status)
               .HasConversion<int>();

            modelBuilder.Entity<UserToDoList>()
                .HasOne(t => t.User)
                .WithMany(u => u.ToDoLists)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskReport>()
               .HasOne(tr => tr.Task)
               .WithMany(t => t.TaskReports)
               .HasForeignKey(tr => tr.TaskId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskReport>()
                .HasOne(tr => tr.CreatedByUser)
                .WithMany(u => u.TaskReports)
                .HasForeignKey(tr => tr.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskProcess>()
             .HasOne(tp => tp.Task)
             .WithMany(t => t.TaskProcesses)
             .HasForeignKey(tp => tp.TaskId)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserBadge>()
                .HasOne(ub => ub.User)
                .WithMany(u => u.UserBadges)
                .HasForeignKey(ub => ub.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserBadge>()
                .HasOne(ub => ub.Badge)
                .WithMany()
                .HasForeignKey(ub => ub.BadgeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Information>()
                .HasOne(i => i.CreatedByUser)
                .WithMany()
                .HasForeignKey(i => i.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
               .HasOne(m => m.Sender)
               .WithMany(u => u.SentMessages)
               .HasForeignKey(m => m.SenderId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
              .Property(m => m.Timestamp)
              .HasDefaultValueSql("GETDATE()");
        }
    }
}
