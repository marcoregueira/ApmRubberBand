using H2h.RubberBand.Database.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace H2h.RubberBand.Database.SqlServer.Context
{
    public class SqlServerContextFactory : IDesignTimeDbContextFactory<SqlServerContext>
    {
        public SqlServerContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BaseContext>()
                .UseSqlServer("empty")
                .Options;

            return new SqlServerContext(optionsBuilder);
        }
    }
}
