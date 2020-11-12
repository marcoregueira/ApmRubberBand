using Microsoft.EntityFrameworkCore.Migrations;

namespace H2h.RubberBand.Database.SqlServer.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "Apm_HiloSequence",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "apm_client_configuration",
                columns: table => new
                {
                    LineId = table.Column<long>(nullable: false),
                    ServiceName = table.Column<string>(nullable: true),
                    ServiceEnvironment = table.Column<string>(nullable: true),
                    MaxAgeSeconds = table.Column<int>(nullable: false),
                    OptionValues = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apm_client_configuration", x => x.LineId);
                });

            migrationBuilder.CreateTable(
                name: "apm_errors",
                columns: table => new
                {
                    LineId = table.Column<long>(nullable: false),
                    Time = table.Column<byte[]>(type: "timestamp", nullable: false),
                    Host = table.Column<string>(nullable: true),
                    App = table.Column<string>(nullable: true),
                    ErrorId = table.Column<string>(nullable: true),
                    TransactionId = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apm_errors", x => x.LineId);
                });

            migrationBuilder.CreateTable(
                name: "apm_log",
                columns: table => new
                {
                    LineId = table.Column<long>(nullable: false),
                    Time = table.Column<byte[]>(type: "timestamp", nullable: false),
                    Host = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    Database = table.Column<string>(nullable: true),
                    RemoteHost = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Duration = table.Column<decimal>(nullable: false),
                    LogId = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    TransactionId = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    App = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apm_log", x => x.LineId);
                });

            migrationBuilder.CreateTable(
                name: "apm_metrics",
                columns: table => new
                {
                    LineId = table.Column<long>(nullable: false),
                    Time = table.Column<byte[]>(type: "timestamp", nullable: false),
                    Host = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apm_metrics", x => x.LineId);
                });

            migrationBuilder.CreateTable(
                name: "apm_transaction",
                columns: table => new
                {
                    LineId = table.Column<long>(nullable: false),
                    Time = table.Column<byte[]>(type: "timestamp", nullable: false),
                    Host = table.Column<string>(nullable: true),
                    App = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Result = table.Column<string>(nullable: true),
                    TransactionType = table.Column<string>(nullable: true),
                    Database = table.Column<string>(nullable: true),
                    RemoteHost = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    UserEmail = table.Column<string>(nullable: true),
                    Duration = table.Column<decimal>(nullable: false),
                    Id = table.Column<string>(nullable: true),
                    ParentId = table.Column<string>(nullable: true),
                    TransactionId = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apm_transaction", x => x.LineId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_apm_client_configuration_ServiceName_ServiceEnvironment",
                table: "apm_client_configuration",
                columns: new[] { "ServiceName", "ServiceEnvironment" },
                unique: true,
                filter: "[ServiceName] IS NOT NULL AND [ServiceEnvironment] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_apm_errors_Time",
                table: "apm_errors",
                column: "Time");

            migrationBuilder.CreateIndex(
                name: "IX_apm_log_Time",
                table: "apm_log",
                column: "Time");

            migrationBuilder.CreateIndex(
                name: "IX_apm_metrics_Time",
                table: "apm_metrics",
                column: "Time");

            migrationBuilder.CreateIndex(
                name: "IX_apm_transaction_Time",
                table: "apm_transaction",
                column: "Time");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "apm_client_configuration");

            migrationBuilder.DropTable(
                name: "apm_errors");

            migrationBuilder.DropTable(
                name: "apm_log");

            migrationBuilder.DropTable(
                name: "apm_metrics");

            migrationBuilder.DropTable(
                name: "apm_transaction");

            migrationBuilder.DropSequence(
                name: "Apm_HiloSequence");
        }
    }
}