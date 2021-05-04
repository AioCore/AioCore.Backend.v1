using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AioCore.API.Migrations
{
    public partial class CreateSecurityEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SecurityGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: false),
                    IndexLeft = table.Column<int>(type: "integer", nullable: false),
                    IndexRight = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecurityGroups_SecurityGroups_ParentId",
                        column: x => x.ParentId,
                        principalTable: "SecurityGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecurityPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RecordId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SecurityPolicies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Controller = table.Column<string>(type: "text", nullable: true),
                    Action = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityPolicies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SecurityUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecurityUsers_SettingTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "SettingTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecurityPermissionSets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Policy = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityPermissionSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecurityPermissionSets_SecurityPermissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "SecurityPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SecurityGroups_ParentId",
                table: "SecurityGroups",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityPermissionSets_PermissionId",
                table: "SecurityPermissionSets",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityUsers_TenantId",
                table: "SecurityUsers",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SecurityGroups");

            migrationBuilder.DropTable(
                name: "SecurityPermissionSets");

            migrationBuilder.DropTable(
                name: "SecurityPolicies");

            migrationBuilder.DropTable(
                name: "SecurityUsers");

            migrationBuilder.DropTable(
                name: "SecurityPermissions");
        }
    }
}
