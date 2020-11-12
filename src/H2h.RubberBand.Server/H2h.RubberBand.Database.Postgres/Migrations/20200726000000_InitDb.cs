using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

namespace H2h.RubberBand.Database.Postgres.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateSequence(
                name: "EntityFrameworkHiLoSequence",
                schema: "public",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "apm_errors",
                schema: "public",
                columns: table => new
                {
                    LineId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo),
                    Time = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Host = table.Column<string>(nullable: true),
                    App = table.Column<string>(nullable: true),
                    ErrorId = table.Column<string>(nullable: true),
                    TransactionId = table.Column<string>(nullable: true),
                    Data = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apm_errors", x => x.LineId);
                });

            migrationBuilder.CreateTable(
                name: "apm_log",
                schema: "public",
                columns: table => new
                {
                    LineId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo),
                    Time = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Host = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    Database = table.Column<string>(nullable: true),
                    RemoteHost = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Duration = table.Column<decimal>(nullable: false),
                    LogId = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    TransactionId = table.Column<string>(nullable: true),
                    Data = table.Column<string>(type: "jsonb", nullable: true),
                    App = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apm_log", x => x.LineId);
                });

            migrationBuilder.CreateTable(
                name: "apm_metrics",
                schema: "public",
                columns: table => new
                {
                    LineId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo),
                    Time = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Host = table.Column<string>(nullable: true),
                    Data = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apm_metrics", x => x.LineId);
                });

            migrationBuilder.CreateTable(
                name: "apm_transaction",
                schema: "public",
                columns: table => new
                {
                    LineId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo),
                    Time = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Host = table.Column<string>(nullable: true),
                    App = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Result = table.Column<string>(nullable: true),
                    TransactionType = table.Column<string>(nullable: true),
                    Database = table.Column<string>(nullable: true),
                    RemoteHost = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Duration = table.Column<decimal>(nullable: false),
                    Id = table.Column<string>(nullable: true),
                    ParentId = table.Column<string>(nullable: true),
                    TransactionId = table.Column<string>(nullable: true),
                    Data = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apm_transaction", x => x.LineId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_apm_errors_Time",
                schema: "public",
                table: "apm_errors",
                column: "Time");

            migrationBuilder.CreateIndex(
                name: "IX_apm_log_Time",
                schema: "public",
                table: "apm_log",
                column: "Time");

            migrationBuilder.CreateIndex(
                name: "IX_apm_metrics_Time",
                schema: "public",
                table: "apm_metrics",
                column: "Time");

            migrationBuilder.CreateIndex(
                name: "IX_apm_transaction_Time",
                schema: "public",
                table: "apm_transaction",
                column: "Time");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "apm_errors",
                schema: "public");

            migrationBuilder.DropTable(
                name: "apm_log",
                schema: "public");

            migrationBuilder.DropTable(
                name: "apm_metrics",
                schema: "public");

            migrationBuilder.DropTable(
                name: "apm_transaction",
                schema: "public");

            migrationBuilder.DropSequence(
                name: "EntityFrameworkHiLoSequence",
                schema: "public");
        }
    }
}