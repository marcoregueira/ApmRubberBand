using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H2h.RubberBand.Database.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class KnownMetrics1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "System_memory_total",
                schema: "public",
                table: "apm_metrics",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "System_process_cgroup_memory_stats_inactive_file_bytes",
                schema: "public",
                table: "apm_metrics",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "System_memory_total",
                schema: "public",
                table: "apm_metrics");

            migrationBuilder.DropColumn(
                name: "System_process_cgroup_memory_stats_inactive_file_bytes",
                schema: "public",
                table: "apm_metrics");
        }
    }
}
