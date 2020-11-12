using H2h.RubberBand.Database.Postgres.CustomMigrationCode;
using Microsoft.EntityFrameworkCore.Migrations;

namespace H2h.RubberBand.Database.Postgres.Migrations
{
    public partial class InitTimescaleDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Custom.EnableTimescaleDbIfAvailable(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // No way. If Timescaledb is enabled, drop the database and start again
        }
    }
}