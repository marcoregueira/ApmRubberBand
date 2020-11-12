using H2h.RubberBand.Database.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace H2h.RubberBand.Database.SqlServer.Context
{
    public class SqlServerContext : BaseContext
    {
        /*
		 dotnet ef migrations add AddAppField --context PostgresContext --output-dir Migrations/PostgresDatabase
		 dotnet ef database update --context PostgresContext
		*/

        private readonly IConfiguration configuration;

        public SqlServerContext(IConfiguration configuration) : base()
        {
            this.configuration = configuration;
        }

        internal SqlServerContext(DbContextOptions<BaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseHiLo("Apm_HiloSequence");
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (configuration != null)
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("Sqlserver"),

                    options => options
                        .MigrationsHistoryTable("_EFMigrations", "public")
                        .MigrationsAssembly(GetType().GetTypeInfo().Assembly.FullName));

                //sqlServerOptions.EnableRetryOnFailure(); <--- NO SOPORTA TRANSACCIONES DE USUARIO
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}