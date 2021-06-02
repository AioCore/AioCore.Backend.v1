using Microsoft.EntityFrameworkCore.Migrations;

namespace AioCore.API.Migrations
{
    public partial class UpdatePropertiesForSettingFieldAndSettingComponent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowSearch",
                table: "SettingFields",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AllowSequence",
                table: "SettingFields",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Caption",
                table: "SettingComponents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClassName",
                table: "SettingComponents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Settings",
                table: "SettingComponents",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowSearch",
                table: "SettingFields");

            migrationBuilder.DropColumn(
                name: "AllowSequence",
                table: "SettingFields");

            migrationBuilder.DropColumn(
                name: "Caption",
                table: "SettingComponents");

            migrationBuilder.DropColumn(
                name: "ClassName",
                table: "SettingComponents");

            migrationBuilder.DropColumn(
                name: "Settings",
                table: "SettingComponents");
        }
    }
}
