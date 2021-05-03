using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AioCore.API.Migrations
{
    public partial class UpdatePropertiesOfSettingFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "SettingFeatures",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LayoutId",
                table: "SettingFeatures",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SettingFeatures",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "SettingFeatures",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "XmlPage",
                table: "SettingFeatures",
                type: "xml",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SettingFeatures_LayoutId",
                table: "SettingFeatures",
                column: "LayoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_SettingFeatures_SettingLayouts_LayoutId",
                table: "SettingFeatures",
                column: "LayoutId",
                principalTable: "SettingLayouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SettingFeatures_SettingLayouts_LayoutId",
                table: "SettingFeatures");

            migrationBuilder.DropIndex(
                name: "IX_SettingFeatures_LayoutId",
                table: "SettingFeatures");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "SettingFeatures");

            migrationBuilder.DropColumn(
                name: "LayoutId",
                table: "SettingFeatures");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SettingFeatures");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "SettingFeatures");

            migrationBuilder.DropColumn(
                name: "XmlPage",
                table: "SettingFeatures");
        }
    }
}
