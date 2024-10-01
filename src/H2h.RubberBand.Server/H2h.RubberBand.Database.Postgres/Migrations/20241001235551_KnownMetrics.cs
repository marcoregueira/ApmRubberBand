using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H2h.RubberBand.Database.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class KnownMetrics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "System_cpu_total_norm_pct",
                schema: "public",
                table: "apm_metrics",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "System_memory_actual_free",
                schema: "public",
                table: "apm_metrics",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "System_process_cgroup_memory_mem_usage_bytes",
                schema: "public",
                table: "apm_metrics",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "System_process_cpu_total_norm_pct",
                schema: "public",
                table: "apm_metrics",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "System_process_memory_rss_bytes",
                schema: "public",
                table: "apm_metrics",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "System_process_memory_size",
                schema: "public",
                table: "apm_metrics",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "System_cpu_total_norm_pct",
                schema: "public",
                table: "apm_metrics");

            migrationBuilder.DropColumn(
                name: "System_memory_actual_free",
                schema: "public",
                table: "apm_metrics");

            migrationBuilder.DropColumn(
                name: "System_process_cgroup_memory_mem_usage_bytes",
                schema: "public",
                table: "apm_metrics");

            migrationBuilder.DropColumn(
                name: "System_process_cpu_total_norm_pct",
                schema: "public",
                table: "apm_metrics");

            migrationBuilder.DropColumn(
                name: "System_process_memory_rss_bytes",
                schema: "public",
                table: "apm_metrics");

            migrationBuilder.DropColumn(
                name: "System_process_memory_size",
                schema: "public",
                table: "apm_metrics");
        }
    }
}
