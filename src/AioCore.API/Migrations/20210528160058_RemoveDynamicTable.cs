using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AioCore.API.Migrations
{
    public partial class RemoveDynamicTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DynamicBinaries");

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

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SystemTenants",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Database",
                table: "SystemTenants",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "SystemTenants",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Schema",
                table: "SystemTenants",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Server",
                table: "SystemTenants",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "SystemTenants",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Database",
                table: "SystemTenants");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "SystemTenants");

            migrationBuilder.DropColumn(
                name: "Schema",
                table: "SystemTenants");

            migrationBuilder.DropColumn(
                name: "Server",
                table: "SystemTenants");

            migrationBuilder.DropColumn(
                name: "User",
                table: "SystemTenants");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SystemTenants",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DynamicBinaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    ContentType = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DestinationName = table.Column<string>(type: "text", nullable: true),
                    FilePath = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    SizeType = table.Column<int>(type: "integer", nullable: false),
                    SourceName = table.Column<string>(type: "text", nullable: true),
                    ViewCount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicBinaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicBinaries_SystemUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "SystemUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Data = table.Column<string>(type: "xml", nullable: true),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_DynamicBinaries_AuthorId",
                table: "DynamicBinaries",
                column: "AuthorId");

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
        }
    }
}
