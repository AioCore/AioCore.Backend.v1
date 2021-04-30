using Microsoft.EntityFrameworkCore.Migrations;

namespace AioCore.API.Migrations
{
    public partial class AddTenantRef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SettingDom_SettingDom_ParentId",
                table: "SettingDom");

            migrationBuilder.DropForeignKey(
                name: "FK_SettingDom_SettingFeature_FeatureId",
                table: "SettingDom");

            migrationBuilder.DropForeignKey(
                name: "FK_SettingEntity_SettingTenant_TenantId",
                table: "SettingEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SettingView",
                table: "SettingView");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SettingTenant",
                table: "SettingTenant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SettingForm",
                table: "SettingForm");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SettingField",
                table: "SettingField");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SettingFeature",
                table: "SettingFeature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SettingEntity",
                table: "SettingEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SettingDom",
                table: "SettingDom");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SettingAction",
                table: "SettingAction");

            migrationBuilder.RenameTable(
                name: "SettingView",
                newName: "SettingViews");

            migrationBuilder.RenameTable(
                name: "SettingTenant",
                newName: "SettingTenants");

            migrationBuilder.RenameTable(
                name: "SettingForm",
                newName: "SettingForms");

            migrationBuilder.RenameTable(
                name: "SettingField",
                newName: "SettingFields");

            migrationBuilder.RenameTable(
                name: "SettingFeature",
                newName: "SettingFeatures");

            migrationBuilder.RenameTable(
                name: "SettingEntity",
                newName: "SettingEntities");

            migrationBuilder.RenameTable(
                name: "SettingDom",
                newName: "SettingDoms");

            migrationBuilder.RenameTable(
                name: "SettingAction",
                newName: "SettingActions");

            migrationBuilder.RenameIndex(
                name: "IX_SettingEntity_TenantId",
                table: "SettingEntities",
                newName: "IX_SettingEntities_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_SettingDom_ParentId",
                table: "SettingDoms",
                newName: "IX_SettingDoms_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_SettingDom_FeatureId",
                table: "SettingDoms",
                newName: "IX_SettingDoms_FeatureId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SettingViews",
                table: "SettingViews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SettingTenants",
                table: "SettingTenants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SettingForms",
                table: "SettingForms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SettingFields",
                table: "SettingFields",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SettingFeatures",
                table: "SettingFeatures",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SettingEntities",
                table: "SettingEntities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SettingDoms",
                table: "SettingDoms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SettingActions",
                table: "SettingActions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SettingDoms_SettingDoms_ParentId",
                table: "SettingDoms",
                column: "ParentId",
                principalTable: "SettingDoms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SettingDoms_SettingFeatures_FeatureId",
                table: "SettingDoms",
                column: "FeatureId",
                principalTable: "SettingFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SettingEntities_SettingTenants_TenantId",
                table: "SettingEntities",
                column: "TenantId",
                principalTable: "SettingTenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SettingDoms_SettingDoms_ParentId",
                table: "SettingDoms");

            migrationBuilder.DropForeignKey(
                name: "FK_SettingDoms_SettingFeatures_FeatureId",
                table: "SettingDoms");

            migrationBuilder.DropForeignKey(
                name: "FK_SettingEntities_SettingTenants_TenantId",
                table: "SettingEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SettingViews",
                table: "SettingViews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SettingTenants",
                table: "SettingTenants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SettingForms",
                table: "SettingForms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SettingFields",
                table: "SettingFields");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SettingFeatures",
                table: "SettingFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SettingEntities",
                table: "SettingEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SettingDoms",
                table: "SettingDoms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SettingActions",
                table: "SettingActions");

            migrationBuilder.RenameTable(
                name: "SettingViews",
                newName: "SettingView");

            migrationBuilder.RenameTable(
                name: "SettingTenants",
                newName: "SettingTenant");

            migrationBuilder.RenameTable(
                name: "SettingForms",
                newName: "SettingForm");

            migrationBuilder.RenameTable(
                name: "SettingFields",
                newName: "SettingField");

            migrationBuilder.RenameTable(
                name: "SettingFeatures",
                newName: "SettingFeature");

            migrationBuilder.RenameTable(
                name: "SettingEntities",
                newName: "SettingEntity");

            migrationBuilder.RenameTable(
                name: "SettingDoms",
                newName: "SettingDom");

            migrationBuilder.RenameTable(
                name: "SettingActions",
                newName: "SettingAction");

            migrationBuilder.RenameIndex(
                name: "IX_SettingEntities_TenantId",
                table: "SettingEntity",
                newName: "IX_SettingEntity_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_SettingDoms_ParentId",
                table: "SettingDom",
                newName: "IX_SettingDom_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_SettingDoms_FeatureId",
                table: "SettingDom",
                newName: "IX_SettingDom_FeatureId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SettingView",
                table: "SettingView",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SettingTenant",
                table: "SettingTenant",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SettingForm",
                table: "SettingForm",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SettingField",
                table: "SettingField",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SettingFeature",
                table: "SettingFeature",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SettingEntity",
                table: "SettingEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SettingDom",
                table: "SettingDom",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SettingAction",
                table: "SettingAction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SettingDom_SettingDom_ParentId",
                table: "SettingDom",
                column: "ParentId",
                principalTable: "SettingDom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SettingDom_SettingFeature_FeatureId",
                table: "SettingDom",
                column: "FeatureId",
                principalTable: "SettingFeature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SettingEntity_SettingTenant_TenantId",
                table: "SettingEntity",
                column: "TenantId",
                principalTable: "SettingTenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
