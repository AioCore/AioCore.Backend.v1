using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AioCore.API.Migrations
{
    public partial class AddEntityTypeConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "DynamicStringValues",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "DynamicIntegerValues",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "Value",
                table: "DynamicGuidValues",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<float>(
                name: "Value",
                table: "DynamicFloatValues",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "DynamicEntities",
                type: "xml",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "DynamicEntities",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Value",
                table: "DynamicDateValues",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateTable(
                name: "SettingAction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SettingFeature",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingFeature", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SettingField",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingField", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SettingForm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingForm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SettingTenant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FaviconId = table.Column<Guid>(type: "uuid", nullable: false),
                    LogoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingTenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SettingView",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingView", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SettingDom",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TagName = table.Column<string>(type: "text", nullable: true),
                    Attributes = table.Column<string>(type: "xml", nullable: true),
                    AttributeValues = table.Column<string>(type: "xml", nullable: true),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: false),
                    FeatureId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingDom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettingDom_SettingDom_ParentId",
                        column: x => x.ParentId,
                        principalTable: "SettingDom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SettingDom_SettingFeature_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "SettingFeature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SettingEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettingEntity_SettingTenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "SettingTenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SettingDom_FeatureId",
                table: "SettingDom",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_SettingDom_ParentId",
                table: "SettingDom",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SettingEntity_TenantId",
                table: "SettingEntity",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SettingAction");

            migrationBuilder.DropTable(
                name: "SettingDom");

            migrationBuilder.DropTable(
                name: "SettingEntity");

            migrationBuilder.DropTable(
                name: "SettingField");

            migrationBuilder.DropTable(
                name: "SettingForm");

            migrationBuilder.DropTable(
                name: "SettingView");

            migrationBuilder.DropTable(
                name: "SettingFeature");

            migrationBuilder.DropTable(
                name: "SettingTenant");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "DynamicStringValues");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "DynamicIntegerValues");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "DynamicGuidValues");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "DynamicFloatValues");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "DynamicEntities");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "DynamicEntities");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "DynamicDateValues");
        }
    }
}
