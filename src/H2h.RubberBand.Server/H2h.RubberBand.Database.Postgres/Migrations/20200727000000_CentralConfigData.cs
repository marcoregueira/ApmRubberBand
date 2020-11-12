using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace H2h.RubberBand.Database.Postgres.Migrations
{
    public partial class CentralConfigData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "apm_client_configuration",
                schema: "public",
                columns: table => new
                {
                    LineId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo),
                    ServiceName = table.Column<string>(nullable: true),
                    ServiceEnvironment = table.Column<string>(nullable: true),
                    MaxAgeSeconds = table.Column<int>(nullable: false),
                    OptionValues = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apm_client_configuration", x => x.LineId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_apm_client_configuration_ServiceName_ServiceEnvironment",
                schema: "public",
                table: "apm_client_configuration",
                columns: new[] { "ServiceName", "ServiceEnvironment" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "apm_client_configuration",
                schema: "public");
        }
    }
}