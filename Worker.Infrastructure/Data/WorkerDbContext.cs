using Microsoft.EntityFrameworkCore;
using Worker.Core.Models;

namespace Worker.Infrastructure.Data;

public class WorkerDbContext(DbContextOptions<WorkerDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Job>().HasKey(j => j.Id);

        modelBuilder.Entity<ScheduledJob>().ToTable("scheduled_jobs");
        modelBuilder.Entity<QueueJob>().ToTable("queue_jobs");
        modelBuilder.Entity<Queue>().ToTable("queues");

        modelBuilder.Entity<ScheduledJob>(entity =>
        {
            entity.HasIndex(sj => sj.Name).IsUnique();
        });

        modelBuilder.Entity<QueueJob>(entity =>
        {
            entity.HasIndex(qj => new { qj.Status, qj.CreatedAt });
            entity.Property(qj => qj.Status).HasConversion<int>();
        });

        modelBuilder.Entity<Queue>(entity =>
        {
            entity.HasIndex(q => q.Name).IsUnique();
            entity.HasMany(q => q.Jobs)
                .WithOne()
                .HasForeignKey(qj => qj.QueueId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}