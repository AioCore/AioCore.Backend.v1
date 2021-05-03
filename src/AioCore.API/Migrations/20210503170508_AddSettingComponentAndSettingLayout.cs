using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AioCore.API.Migrations
{
    public partial class AddSettingComponentAndSettingLayout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SettingViews",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SettingViews",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentId",
                table: "SettingDoms",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SettingLayoutId",
                table: "SettingDoms",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SettingLayouts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingLayouts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SettingComponents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentType = table.Column<int>(type: "integer", nullable: false),
                    ComponentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ComponentType = table.Column<int>(type: "integer", nullable: false),
                    SettingLayoutId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettingComponents_SettingLayouts_SettingLayoutId",
                        column: x => x.SettingLayoutId,
                        principalTable: "SettingLayouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SettingDoms_ComponentId",
                table: "SettingDoms",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_SettingDoms_SettingLayoutId",
                table: "SettingDoms",
                column: "SettingLayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_SettingComponents_SettingLayoutId",
                table: "SettingComponents",
                column: "SettingLayoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_SettingDoms_SettingComponents_ComponentId",
                table: "SettingDoms",
                column: "ComponentId",
                principalTable: "SettingComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SettingDoms_SettingLayouts_SettingLayoutId",
                table: "SettingDoms",
                column: "SettingLayoutId",
                principalTable: "SettingLayouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SettingDoms_SettingComponents_ComponentId",
                table: "SettingDoms");

            migrationBuilder.DropForeignKey(
                name: "FK_SettingDoms_SettingLayouts_SettingLayoutId",
                table: "SettingDoms");

            migrationBuilder.DropTable(
                name: "SettingComponents");

            migrationBuilder.DropTable(
                name: "SettingLayouts");

            migrationBuilder.DropIndex(
                name: "IX_SettingDoms_ComponentId",
                table: "SettingDoms");

            migrationBuilder.DropIndex(
                name: "IX_SettingDoms_SettingLayoutId",
                table: "SettingDoms");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SettingViews");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SettingViews");

            migrationBuilder.DropColumn(
                name: "ComponentId",
                table: "SettingDoms");

            migrationBuilder.DropColumn(
                name: "SettingLayoutId",
                table: "SettingDoms");
        }
    }
}
