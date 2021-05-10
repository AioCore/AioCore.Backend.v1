using Microsoft.EntityFrameworkCore.Migrations;

namespace AioCore.API.Migrations
{
    public partial class ChangeFileNameAndAddSourceNameAndDestinationName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "DynamicBinaries",
                newName: "SourceName");

            migrationBuilder.AddColumn<string>(
                name: "DestinationName",
                table: "DynamicBinaries",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestinationName",
                table: "DynamicBinaries");

            migrationBuilder.RenameColumn(
                name: "SourceName",
                table: "DynamicBinaries",
                newName: "FileName");
        }
    }
}
