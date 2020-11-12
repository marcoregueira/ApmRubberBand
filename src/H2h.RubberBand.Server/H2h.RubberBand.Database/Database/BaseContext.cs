using H2h.RubberBand.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace H2h.RubberBand.Database.Database
{
    public class BaseContext : DbContext
    {
        public BaseContext() : base()
        {
        }

        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {
        }

        public DbSet<MetricEntity> Metrics { get; set; }
        public DbSet<LogEntity> Logs { get; set; }
        public DbSet<ErrorEntity> Errors { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }
        public DbSet<ClientConfigEntity> ClientConfig { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            modelBuilder.Entity<LogEntity>().HasIndex(x => x.Time);
            modelBuilder.Entity<MetricEntity>().HasIndex(x => x.Time);
            modelBuilder.Entity<ErrorEntity>().HasIndex(x => x.Time);
            modelBuilder.Entity<TransactionEntity>().HasIndex(x => x.Time);
            modelBuilder.Entity<ClientConfigEntity>()
                .HasIndex(x => new { x.ServiceName, x.ServiceEnvironment })
                .IsUnique();
        }
    }
}