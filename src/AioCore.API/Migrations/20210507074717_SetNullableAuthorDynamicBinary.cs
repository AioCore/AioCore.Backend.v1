using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AioCore.API.Migrations
{
    public partial class SetNullableAuthorDynamicBinary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DynamicBinaries_SecurityUsers_AuthorId",
                table: "DynamicBinaries");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "DynamicBinaries",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicBinaries_SecurityUsers_AuthorId",
                table: "DynamicBinaries",
                column: "AuthorId",
                principalTable: "SecurityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DynamicBinaries_SecurityUsers_AuthorId",
                table: "DynamicBinaries");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "DynamicBinaries",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicBinaries_SecurityUsers_AuthorId",
                table: "DynamicBinaries",
                column: "AuthorId",
                principalTable: "SecurityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
