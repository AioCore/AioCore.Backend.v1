﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AioCore.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DynamicDateAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicDateAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DynamicEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Data = table.Column<string>(type: "xml", nullable: true),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DynamicFloatAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicFloatAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DynamicGuidAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicGuidAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DynamicIntegerAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicIntegerAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DynamicStringAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicStringAttributes", x => x.Id);
                });

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
                name: "SettingActions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SettingFields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FieldType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingFields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SettingForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingForms", x => x.Id);
                });

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
                name: "SettingTenants",
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
                    table.PrimaryKey("PK_SettingTenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SettingViews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingViews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DynamicDateValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicDateValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicDateValues_DynamicDateAttributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "DynamicDateAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DynamicDateValues_DynamicEntities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "DynamicEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DynamicFloatValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicFloatValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicFloatValues_DynamicEntities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "DynamicEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DynamicFloatValues_DynamicFloatAttributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "DynamicFloatAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DynamicGuidValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicGuidValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicGuidValues_DynamicEntities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "DynamicEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DynamicGuidValues_DynamicGuidAttributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "DynamicGuidAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DynamicIntegerValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicIntegerValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicIntegerValues_DynamicEntities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "DynamicEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DynamicIntegerValues_DynamicIntegerAttributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "DynamicIntegerAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DynamicStringValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicStringValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicStringValues_DynamicEntities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "DynamicEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DynamicStringValues_DynamicStringAttributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "DynamicStringAttributes",
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

            migrationBuilder.CreateTable(
                name: "SettingFeatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Slug = table.Column<string>(type: "text", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    XmlPage = table.Column<string>(type: "xml", nullable: true),
                    LayoutId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettingFeatures_SettingLayouts_LayoutId",
                        column: x => x.LayoutId,
                        principalTable: "SettingLayouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "SettingEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettingEntities_SettingTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "SettingTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SettingDoms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TagName = table.Column<string>(type: "text", nullable: true),
                    Attributes = table.Column<string>(type: "xml", nullable: true),
                    AttributeValues = table.Column<string>(type: "xml", nullable: true),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ComponentId = table.Column<Guid>(type: "uuid", nullable: false),
                    FeatureId = table.Column<Guid>(type: "uuid", nullable: false),
                    SettingLayoutId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingDoms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettingDoms_SettingComponents_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "SettingComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SettingDoms_SettingDoms_ParentId",
                        column: x => x.ParentId,
                        principalTable: "SettingDoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SettingDoms_SettingFeatures_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "SettingFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SettingDoms_SettingLayouts_SettingLayoutId",
                        column: x => x.SettingLayoutId,
                        principalTable: "SettingLayouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DynamicDateValues_AttributeId",
                table: "DynamicDateValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicDateValues_EntityId",
                table: "DynamicDateValues",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicFloatValues_AttributeId",
                table: "DynamicFloatValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicFloatValues_EntityId",
                table: "DynamicFloatValues",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicGuidValues_AttributeId",
                table: "DynamicGuidValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicGuidValues_EntityId",
                table: "DynamicGuidValues",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicIntegerValues_AttributeId",
                table: "DynamicIntegerValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicIntegerValues_EntityId",
                table: "DynamicIntegerValues",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicStringValues_AttributeId",
                table: "DynamicStringValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicStringValues_EntityId",
                table: "DynamicStringValues",
                column: "EntityId");

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

            migrationBuilder.CreateIndex(
                name: "IX_SettingComponents_SettingLayoutId",
                table: "SettingComponents",
                column: "SettingLayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_SettingDoms_ComponentId",
                table: "SettingDoms",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_SettingDoms_FeatureId",
                table: "SettingDoms",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_SettingDoms_ParentId",
                table: "SettingDoms",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SettingDoms_SettingLayoutId",
                table: "SettingDoms",
                column: "SettingLayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_SettingEntities_TenantId",
                table: "SettingEntities",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_SettingFeatures_LayoutId",
                table: "SettingFeatures",
                column: "LayoutId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DynamicDateValues");

            migrationBuilder.DropTable(
                name: "DynamicFloatValues");

            migrationBuilder.DropTable(
                name: "DynamicGuidValues");

            migrationBuilder.DropTable(
                name: "DynamicIntegerValues");

            migrationBuilder.DropTable(
                name: "DynamicStringValues");

            migrationBuilder.DropTable(
                name: "SecurityGroups");

            migrationBuilder.DropTable(
                name: "SecurityPermissionSets");

            migrationBuilder.DropTable(
                name: "SecurityPolicies");

            migrationBuilder.DropTable(
                name: "SecurityUsers");

            migrationBuilder.DropTable(
                name: "SettingActions");

            migrationBuilder.DropTable(
                name: "SettingDoms");

            migrationBuilder.DropTable(
                name: "SettingEntities");

            migrationBuilder.DropTable(
                name: "SettingFields");

            migrationBuilder.DropTable(
                name: "SettingForms");

            migrationBuilder.DropTable(
                name: "SettingViews");

            migrationBuilder.DropTable(
                name: "DynamicDateAttributes");

            migrationBuilder.DropTable(
                name: "DynamicFloatAttributes");

            migrationBuilder.DropTable(
                name: "DynamicGuidAttributes");

            migrationBuilder.DropTable(
                name: "DynamicIntegerAttributes");

            migrationBuilder.DropTable(
                name: "DynamicEntities");

            migrationBuilder.DropTable(
                name: "DynamicStringAttributes");

            migrationBuilder.DropTable(
                name: "SecurityPermissions");

            migrationBuilder.DropTable(
                name: "SettingComponents");

            migrationBuilder.DropTable(
                name: "SettingFeatures");

            migrationBuilder.DropTable(
                name: "SettingTenants");

            migrationBuilder.DropTable(
                name: "SettingLayouts");
        }
    }
}
