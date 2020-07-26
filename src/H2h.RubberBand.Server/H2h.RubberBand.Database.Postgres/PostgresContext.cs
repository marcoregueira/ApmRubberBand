using H2h.RubberBand.Database.Database;
using H2h.RubberBand.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

namespace H2h.RubberBand.Database.Postgres
{
    public class PostgresContext : BaseContext
    {
        /*
		 dotnet ef migrations add AddAppField --context PostgresContext --output-dir Migrations/PostgresDatabase
		 dotnet ef database update --context PostgresContext
		*/


        readonly IConfiguration configuration;

        public PostgresContext(IConfiguration configuration) : base()
        {
            this.configuration = configuration;
        }

        internal PostgresContext(DbContextOptions<BaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.UseHiLo();

            modelBuilder.Entity<LogEntity>()
                .Property(x => x.Data).HasColumnType("jsonb");

            modelBuilder.Entity<MetricEntity>()
                .Property(x => x.Data).HasColumnType("jsonb");

            modelBuilder.Entity<ErrorEntity>()
                .Property(x => x.Data).HasColumnType("jsonb");

            modelBuilder.Entity<TransactionEntity>()
                .Property(x => x.Data).HasColumnType("jsonb");

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (configuration != null)
            {
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("Postgres"),

                    options => options
                        .MigrationsHistoryTable("_EFMigrations", "public")
                        .MigrationsAssembly(GetType().GetTypeInfo().Assembly.FullName));

                //sqlServerOptions.EnableRetryOnFailure(); <--- NO SOPORTA TRANSACCIONES DE USUARIO
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
