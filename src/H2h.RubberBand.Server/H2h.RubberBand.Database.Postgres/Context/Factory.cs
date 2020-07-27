using H2h.RubberBand.Database.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace H2h.RubberBand.Database.Postgres
{
    public class PostgresContextFactory : IDesignTimeDbContextFactory<PostgresContext>
    {
        public PostgresContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BaseContext>()
                .UseNpgsql("empty")
                .Options;

            return new PostgresContext(optionsBuilder);
        }
    }
}
