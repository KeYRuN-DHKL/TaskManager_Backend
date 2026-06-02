using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Entities;

namespace TaskManager.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();
        public DbSet<AppTaskEntity> AppTasks => Set<AppTaskEntity>();
        public DbSet<TaskTagEntity> TaskTags => Set<TaskTagEntity>();
        public DbSet<TagEntity> Tags => Set<TagEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectEntity>()
                .HasOne(project => project.Owner)
                .WithMany(user => user.Projects)
                .HasForeignKey(project => project.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppTaskEntity>()
                .HasOne(task => task.Project)
                .WithMany(project => project.Tasks)
                .HasForeignKey(task => task.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppTaskEntity>()
                .HasOne(task => task.Assignee)
                .WithMany(assignee => assignee.AssignedTasks)
                .HasForeignKey(task => task.AssigneeId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TaskTagEntity>()
                .HasKey(tt => new { tt.TaskId, tt.TagId });

            modelBuilder.Entity<TaskTagEntity>()
                .HasOne(task => task.Task)
                .WithMany(tag => tag.TaskTag)
                .HasForeignKey(task => task.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskTagEntity>()
                .HasOne(tag => tag.Tag)
                .WithMany(task => task.TaskTag)
                .HasForeignKey(task => task.TagId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserEntity>().HasQueryFilter(user => !user.IsDeleted);
            modelBuilder.Entity<ProjectEntity>().HasQueryFilter(project => !project.IsDeleted);
            modelBuilder.Entity<AppTaskEntity>().HasQueryFilter(task => !task.IsDeleted);
            modelBuilder.Entity<TagEntity>().HasQueryFilter(tasktag => !tasktag.IsDeleted);

            modelBuilder.Entity<AppTaskEntity>()
                .Property(tasks => tasks.Status)
                .HasConversion<string>();

            modelBuilder.Entity<AppTaskEntity>()
                .Property(tasks => tasks.Priority)
                .HasConversion<string>();
        }

        public override int SaveChanges()
        {
            UpdateTimeStamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            UpdateTimeStamps();
            return base.SaveChangesAsync(ct);
        }

        private void UpdateTimeStamps()
        {
            var entries = ChangeTracker.Entries<BaseEntity>()
                            .Where(e => e.State is EntityState.Added or EntityState.Modified);

            foreach(var entry in entries)
            {
                if(entry.State == EntityState.Modified)
                    entry.Entity.UpdatedAt = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                    entry.Entity.CreatedAt = DateTime.UtcNow;
            }
        }
    }
}
