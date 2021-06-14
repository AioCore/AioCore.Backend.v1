using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AioCore.API.Migrations
{
    public partial class RefactorUserTenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemUsers_SystemTenants_TenantId",
                table: "SystemUsers");

            migrationBuilder.DropIndex(
                name: "IX_SystemUsers_TenantId",
                table: "SystemUsers");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "SystemUsers");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "SystemUsers");

            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "SystemUsers",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SystemUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SystemUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                table: "SystemUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SystemUserPolicies",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "SystemUserPolicies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SystemUserPolicies",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                table: "SystemUserPolicies",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SystemUserGroups",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "SystemUserGroups",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SystemUserGroups",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                table: "SystemUserGroups",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LogoId",
                table: "SystemTenants",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "FaviconId",
                table: "SystemTenants",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SystemTenants",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "SystemTenants",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SystemTenants",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                table: "SystemTenants",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SystemTenantApplications",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "SystemTenantApplications",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SystemTenantApplications",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                table: "SystemTenantApplications",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SystemPolicies",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "SystemPolicies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SystemPolicies",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                table: "SystemPolicies",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SystemPermissionSets",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "SystemPermissionSets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SystemPermissionSets",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                table: "SystemPermissionSets",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SystemPermissions",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "SystemPermissions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SystemPermissions",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                table: "SystemPermissions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SystemBinaries",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "SystemBinaries",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SystemBinaries",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                table: "SystemBinaries",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SystemApplications",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "SystemApplications",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SystemApplications",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                table: "SystemApplications",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SettingViews",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "SettingViews",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SettingViews",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                table: "SettingViews",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SettingLayouts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "SettingLayouts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SettingLayouts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                table: "SettingLayouts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SettingForms",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "SettingForms",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SettingForms",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                table: "SettingForms",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SettingFields",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "SettingFields",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SettingFields",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                table: "SettingFields",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SettingEntityTypes",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "SettingEntityTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SettingEntityTypes",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                table: "SettingEntityTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SettingDoms",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "SettingDoms",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SettingDoms",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                table: "SettingDoms",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SettingComponents",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "SettingComponents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SettingComponents",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                table: "SettingComponents",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SettingActions",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "SettingActions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SettingActions",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                table: "SettingActions",
                type: "timestamp with time zone",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemTenantSystemUser");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SystemUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SystemUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SystemUsers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SystemUserPolicies");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SystemUserPolicies");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SystemUserPolicies");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SystemUserPolicies");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SystemUserGroups");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SystemUserGroups");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SystemUserGroups");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SystemUserGroups");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SystemTenants");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SystemTenants");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SystemTenants");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SystemTenants");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SystemTenantApplications");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SystemTenantApplications");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SystemTenantApplications");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SystemTenantApplications");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SystemPolicies");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SystemPolicies");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SystemPolicies");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SystemPolicies");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SystemPermissionSets");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SystemPermissionSets");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SystemPermissionSets");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SystemPermissionSets");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SystemPermissions");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SystemPermissions");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SystemPermissions");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SystemPermissions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SystemBinaries");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SystemBinaries");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SystemBinaries");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SystemBinaries");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SystemApplications");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SystemApplications");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SystemApplications");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SystemApplications");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SettingViews");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SettingViews");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SettingViews");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SettingViews");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SettingLayouts");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SettingLayouts");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SettingLayouts");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SettingLayouts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SettingForms");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SettingForms");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SettingForms");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SettingForms");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SettingFields");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SettingFields");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SettingFields");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SettingFields");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SettingEntityTypes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SettingEntityTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SettingEntityTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SettingEntityTypes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SettingDoms");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SettingDoms");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SettingDoms");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SettingDoms");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SettingComponents");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SettingComponents");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SettingComponents");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SettingComponents");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SettingActions");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SettingActions");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SettingActions");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SettingActions");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "SystemUsers",
                newName: "Modified");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Created",
                table: "SystemUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "SystemUsers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "LogoId",
                table: "SystemTenants",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FaviconId",
                table: "SystemTenants",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemUsers_TenantId",
                table: "SystemUsers",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemUsers_SystemTenants_TenantId",
                table: "SystemUsers",
                column: "TenantId",
                principalTable: "SystemTenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
