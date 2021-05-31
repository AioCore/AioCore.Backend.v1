using System;
using AioCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Package.DatabaseManagement;

namespace AioCore.API.Migrations.AioDynamic
{
    public partial class InitMigration : Migration
    {
        private readonly ISchemaDbContext _schema;

        public InitMigration(ISchemaDbContext schema)
        {
            _schema = schema;
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (!string.IsNullOrEmpty(_schema?.Schema))
            {
                migrationBuilder.EnsureSchema(
                    name: _schema?.Schema);
            }

            migrationBuilder.CreateTable(
                name: "DynamicBinaries",
                schema: _schema?.Schema,
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceName = table.Column<string>(type: "text", nullable: true),
                    DestinationName = table.Column<string>(type: "text", nullable: true),
                    FilePath = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    ViewCount = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    SizeType = table.Column<int>(type: "integer", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicBinaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DynamicDateAttributes",
                schema: _schema?.Schema,
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
                schema: _schema?.Schema,
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
                schema: _schema?.Schema,
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
                schema: _schema?.Schema,
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
                schema: _schema?.Schema,
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
                schema: _schema?.Schema,
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
                schema: _schema?.Schema,
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
                        principalSchema: _schema?.Schema,
                        principalTable: "DynamicDateAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DynamicDateValues_DynamicEntities_EntityId",
                        column: x => x.EntityId,
                        principalSchema: _schema?.Schema,
                        principalTable: "DynamicEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DynamicFloatValues",
                schema: _schema?.Schema,
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
                        principalSchema: _schema?.Schema,
                        principalTable: "DynamicEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DynamicFloatValues_DynamicFloatAttributes_AttributeId",
                        column: x => x.AttributeId,
                        principalSchema: _schema?.Schema,
                        principalTable: "DynamicFloatAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DynamicGuidValues",
                schema: _schema?.Schema,
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
                        principalSchema: _schema?.Schema,
                        principalTable: "DynamicEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DynamicGuidValues_DynamicGuidAttributes_AttributeId",
                        column: x => x.AttributeId,
                        principalSchema: _schema?.Schema,
                        principalTable: "DynamicGuidAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DynamicIntegerValues",
                schema: _schema?.Schema,
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
                        principalSchema: _schema?.Schema,
                        principalTable: "DynamicEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DynamicIntegerValues_DynamicIntegerAttributes_AttributeId",
                        column: x => x.AttributeId,
                        principalSchema: _schema?.Schema,
                        principalTable: "DynamicIntegerAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DynamicStringValues",
                schema: _schema?.Schema,
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
                        principalSchema: _schema?.Schema,
                        principalTable: "DynamicEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DynamicStringValues_DynamicStringAttributes_AttributeId",
                        column: x => x.AttributeId,
                        principalSchema: _schema?.Schema,
                        principalTable: "DynamicStringAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DynamicDateValues_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicDateValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicDateValues_EntityId",
                schema: _schema?.Schema,
                table: "DynamicDateValues",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicFloatValues_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicFloatValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicFloatValues_EntityId",
                schema: _schema?.Schema,
                table: "DynamicFloatValues",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicGuidValues_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicGuidValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicGuidValues_EntityId",
                schema: _schema?.Schema,
                table: "DynamicGuidValues",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicIntegerValues_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicIntegerValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicIntegerValues_EntityId",
                schema: _schema?.Schema,
                table: "DynamicIntegerValues",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicStringValues_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicStringValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicStringValues_EntityId",
                schema: _schema?.Schema,
                table: "DynamicStringValues",
                column: "EntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DynamicBinaries",
                schema: _schema?.Schema);

            migrationBuilder.DropTable(
                name: "DynamicDateValues",
                schema: _schema?.Schema);

            migrationBuilder.DropTable(
                name: "DynamicFloatValues",
                schema: _schema?.Schema);

            migrationBuilder.DropTable(
                name: "DynamicGuidValues",
                schema: _schema?.Schema);

            migrationBuilder.DropTable(
                name: "DynamicIntegerValues",
                schema: _schema?.Schema);

            migrationBuilder.DropTable(
                name: "DynamicStringValues",
                schema: _schema?.Schema);

            migrationBuilder.DropTable(
                name: "DynamicDateAttributes",
                schema: _schema?.Schema);

            migrationBuilder.DropTable(
                name: "DynamicFloatAttributes",
                schema: _schema?.Schema);

            migrationBuilder.DropTable(
                name: "DynamicGuidAttributes",
                schema: _schema?.Schema);

            migrationBuilder.DropTable(
                name: "DynamicIntegerAttributes",
                schema: _schema?.Schema);

            migrationBuilder.DropTable(
                name: "DynamicEntities",
                schema: _schema?.Schema);

            migrationBuilder.DropTable(
                name: "DynamicStringAttributes",
                schema: _schema?.Schema);
        }
    }
}
