using Microsoft.EntityFrameworkCore.Migrations;

namespace AioCore.API.Migrations
{
    public partial class SystemTenantRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Database",
                table: "SystemTenants");

            migrationBuilder.DropColumn(
                name: "DatabaseType",
                table: "SystemTenants");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "SystemTenants");

            migrationBuilder.DropColumn(
                name: "Schema",
                table: "SystemTenants");

            migrationBuilder.DropColumn(
                name: "Server",
                table: "SystemTenants");

            migrationBuilder.DropColumn(
                name: "User",
                table: "SystemTenants");

            migrationBuilder.AddColumn<string>(
                name: "DatabaseInfo",
                table: "SystemTenants",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ElasticsearchInfo",
                table: "SystemTenants",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatabaseInfo",
                table: "SystemTenants");

            migrationBuilder.DropColumn(
                name: "ElasticsearchInfo",
                table: "SystemTenants");

            migrationBuilder.AddColumn<string>(
                name: "Database",
                table: "SystemTenants",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DatabaseType",
                table: "SystemTenants",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "SystemTenants",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Schema",
                table: "SystemTenants",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Server",
                table: "SystemTenants",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "SystemTenants",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
