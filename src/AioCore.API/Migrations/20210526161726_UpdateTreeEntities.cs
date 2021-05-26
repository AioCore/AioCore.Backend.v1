using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AioCore.API.Migrations
{
    public partial class UpdateTreeEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemGroups_SystemGroups_ParentId",
                table: "SystemGroups");

            migrationBuilder.DropTable(
                name: "SystemApplicationTenant");

            migrationBuilder.DropIndex(
                name: "IX_SystemGroups_ParentId",
                table: "SystemGroups");

            migrationBuilder.RenameColumn(
                name: "IndexRight",
                table: "SystemGroups",
                newName: "Right");

            migrationBuilder.RenameColumn(
                name: "IndexLeft",
                table: "SystemGroups",
                newName: "Level");

            migrationBuilder.RenameColumn(
                name: "IndexRight",
                table: "SettingFeatures",
                newName: "Right");

            migrationBuilder.RenameColumn(
                name: "IndexLeft",
                table: "SettingFeatures",
                newName: "Level");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                table: "SystemGroups",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<int>(
                name: "Left",
                table: "SystemGroups",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Moving",
                table: "SystemGroups",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "RootId",
                table: "SystemGroups",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Left",
                table: "SettingFeatures",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Moving",
                table: "SettingFeatures",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "SettingFeatures",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RootId",
                table: "SettingFeatures",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SettingFeatureSettingFeature",
                columns: table => new
                {
                    ChildrenId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescendantsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingFeatureSettingFeature", x => new { x.ChildrenId, x.DescendantsId });
                    table.ForeignKey(
                        name: "FK_SettingFeatureSettingFeature_SettingFeatures_ChildrenId",
                        column: x => x.ChildrenId,
                        principalTable: "SettingFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SettingFeatureSettingFeature_SettingFeatures_DescendantsId",
                        column: x => x.DescendantsId,
                        principalTable: "SettingFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemGroupSystemGroup",
                columns: table => new
                {
                    ChildrenId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescendantsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemGroupSystemGroup", x => new { x.ChildrenId, x.DescendantsId });
                    table.ForeignKey(
                        name: "FK_SystemGroupSystemGroup_SystemGroups_ChildrenId",
                        column: x => x.ChildrenId,
                        principalTable: "SystemGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemGroupSystemGroup_SystemGroups_DescendantsId",
                        column: x => x.DescendantsId,
                        principalTable: "SystemGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemTenantApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemTenantApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemTenantApplications_SystemApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "SystemApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemTenantApplications_SystemTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "SystemTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemUserGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUserGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemUserGroups_SystemGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "SystemGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemUserGroups_SystemUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "SystemUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemUserPolicies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PolicyId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUserPolicies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemUserPolicies_SystemPolicies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "SystemPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemUserPolicies_SystemUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "SystemUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SettingFeatureSettingFeature_DescendantsId",
                table: "SettingFeatureSettingFeature",
                column: "DescendantsId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemGroupSystemGroup_DescendantsId",
                table: "SystemGroupSystemGroup",
                column: "DescendantsId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemTenantApplications_ApplicationId",
                table: "SystemTenantApplications",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemTenantApplications_TenantId",
                table: "SystemTenantApplications",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUserGroups_GroupId",
                table: "SystemUserGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUserGroups_UserId",
                table: "SystemUserGroups",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUserPolicies_PolicyId",
                table: "SystemUserPolicies",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUserPolicies_UserId",
                table: "SystemUserPolicies",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SettingFeatureSettingFeature");

            migrationBuilder.DropTable(
                name: "SystemGroupSystemGroup");

            migrationBuilder.DropTable(
                name: "SystemTenantApplications");

            migrationBuilder.DropTable(
                name: "SystemUserGroups");

            migrationBuilder.DropTable(
                name: "SystemUserPolicies");

            migrationBuilder.DropColumn(
                name: "Left",
                table: "SystemGroups");

            migrationBuilder.DropColumn(
                name: "Moving",
                table: "SystemGroups");

            migrationBuilder.DropColumn(
                name: "RootId",
                table: "SystemGroups");

            migrationBuilder.DropColumn(
                name: "Left",
                table: "SettingFeatures");

            migrationBuilder.DropColumn(
                name: "Moving",
                table: "SettingFeatures");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "SettingFeatures");

            migrationBuilder.DropColumn(
                name: "RootId",
                table: "SettingFeatures");

            migrationBuilder.RenameColumn(
                name: "Right",
                table: "SystemGroups",
                newName: "IndexRight");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "SystemGroups",
                newName: "IndexLeft");

            migrationBuilder.RenameColumn(
                name: "Right",
                table: "SettingFeatures",
                newName: "IndexRight");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "SettingFeatures",
                newName: "IndexLeft");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                table: "SystemGroups",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SystemApplicationTenant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemApplicationTenant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemApplicationTenant_SystemApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "SystemApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemApplicationTenant_SystemTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "SystemTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemGroups_ParentId",
                table: "SystemGroups",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemApplicationTenant_ApplicationId",
                table: "SystemApplicationTenant",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemApplicationTenant_TenantId",
                table: "SystemApplicationTenant",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemGroups_SystemGroups_ParentId",
                table: "SystemGroups",
                column: "ParentId",
                principalTable: "SystemGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
