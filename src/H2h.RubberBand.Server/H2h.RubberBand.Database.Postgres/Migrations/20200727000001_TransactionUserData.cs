using Microsoft.EntityFrameworkCore.Migrations;

namespace H2h.RubberBand.Database.Postgres.Migrations
{
    public partial class TransactionUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "public",
                table: "apm_transaction",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "public",
                table: "apm_transaction",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "public",
                table: "apm_transaction");

            migrationBuilder.DropColumn(
                name: "UserName",
                schema: "public",
                table: "apm_transaction");
        }
    }
}
