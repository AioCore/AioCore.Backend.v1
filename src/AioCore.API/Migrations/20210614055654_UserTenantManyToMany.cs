using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AioCore.API.Migrations
{
    public partial class UserTenantManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemTenantSystemUser");

            migrationBuilder.CreateTable(
                name: "SystemUserTenant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUserTenant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemUserTenant_SystemTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "SystemTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemUserTenant_SystemUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "SystemUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemUserTenant_TenantId",
                table: "SystemUserTenant",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUserTenant_UserId",
                table: "SystemUserTenant",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemUserTenant");

            migrationBuilder.CreateTable(
                name: "SystemTenantSystemUser",
                columns: table => new
                {
                    TenantsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemTenantSystemUser", x => new { x.TenantsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_SystemTenantSystemUser_SystemTenants_TenantsId",
                        column: x => x.TenantsId,
                        principalTable: "SystemTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemTenantSystemUser_SystemUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "SystemUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemTenantSystemUser_UsersId",
                table: "SystemTenantSystemUser",
                column: "UsersId");
        }
    }
}
