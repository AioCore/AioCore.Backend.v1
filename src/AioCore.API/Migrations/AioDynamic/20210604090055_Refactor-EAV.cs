using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Package.DatabaseManagement;

namespace AioCore.API.Migrations.AioDynamic
{
    public partial class RefactorEAV : Migration
    {
        private readonly ISchemaDbContext _schema;

        public RefactorEAV(ISchemaDbContext schema)
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

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicDateValues_DynamicDateAttributes_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicDateValues");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicFloatValues_DynamicFloatAttributes_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicFloatValues");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicGuidValues_DynamicGuidAttributes_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicGuidValues");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicIntegerValues_DynamicIntegerAttributes_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicIntegerValues");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicStringValues_DynamicStringAttributes_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicStringValues");

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
                name: "DynamicStringAttributes",
                schema: _schema?.Schema);

            migrationBuilder.RenameColumn(
                name: "EntityId",
                schema: _schema?.Schema,
                table: "DynamicEntities",
                newName: "EntityTypeId");

            migrationBuilder.AddColumn<Guid>(
                name: "EntityTypeId",
                schema: _schema?.Schema,
                table: "DynamicStringValues",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EntityTypeId",
                schema: _schema?.Schema,
                table: "DynamicIntegerValues",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EntityTypeId",
                schema: _schema?.Schema,
                table: "DynamicGuidValues",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EntityTypeId",
                schema: _schema?.Schema,
                table: "DynamicFloatValues",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EntityTypeId",
                schema: _schema?.Schema,
                table: "DynamicDateValues",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "DynamicAttributes",
                schema: _schema?.Schema,
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    EntityTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicAttributes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicDateValues_DynamicAttributes_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicDateValues",
                column: "AttributeId",
                principalSchema: _schema?.Schema,
                principalTable: "DynamicAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicFloatValues_DynamicAttributes_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicFloatValues",
                column: "AttributeId",
                principalSchema: _schema?.Schema,
                principalTable: "DynamicAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicGuidValues_DynamicAttributes_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicGuidValues",
                column: "AttributeId",
                principalSchema: _schema?.Schema,
                principalTable: "DynamicAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicIntegerValues_DynamicAttributes_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicIntegerValues",
                column: "AttributeId",
                principalSchema: _schema?.Schema,
                principalTable: "DynamicAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicStringValues_DynamicAttributes_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicStringValues",
                column: "AttributeId",
                principalSchema: _schema?.Schema,
                principalTable: "DynamicAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DynamicDateValues_DynamicAttributes_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicDateValues");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicFloatValues_DynamicAttributes_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicFloatValues");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicGuidValues_DynamicAttributes_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicGuidValues");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicIntegerValues_DynamicAttributes_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicIntegerValues");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicStringValues_DynamicAttributes_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicStringValues");

            migrationBuilder.DropTable(
                name: "DynamicAttributes",
                schema: _schema?.Schema);

            migrationBuilder.DropColumn(
                name: "EntityTypeId",
                schema: _schema?.Schema,
                table: "DynamicStringValues");

            migrationBuilder.DropColumn(
                name: "EntityTypeId",
                schema: _schema?.Schema,
                table: "DynamicIntegerValues");

            migrationBuilder.DropColumn(
                name: "EntityTypeId",
                schema: _schema?.Schema,
                table: "DynamicGuidValues");

            migrationBuilder.DropColumn(
                name: "EntityTypeId",
                schema: _schema?.Schema,
                table: "DynamicFloatValues");

            migrationBuilder.DropColumn(
                name: "EntityTypeId",
                schema: _schema?.Schema,
                table: "DynamicDateValues");

            migrationBuilder.RenameColumn(
                name: "EntityTypeId",
                schema: _schema?.Schema,
                table: "DynamicEntities",
                newName: "EntityId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicDateValues_DynamicDateAttributes_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicDateValues",
                column: "AttributeId",
                principalSchema: _schema?.Schema,
                principalTable: "DynamicDateAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicFloatValues_DynamicFloatAttributes_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicFloatValues",
                column: "AttributeId",
                principalSchema: _schema?.Schema,
                principalTable: "DynamicFloatAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicGuidValues_DynamicGuidAttributes_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicGuidValues",
                column: "AttributeId",
                principalSchema: _schema?.Schema,
                principalTable: "DynamicGuidAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicIntegerValues_DynamicIntegerAttributes_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicIntegerValues",
                column: "AttributeId",
                principalSchema: _schema?.Schema,
                principalTable: "DynamicIntegerAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicStringValues_DynamicStringAttributes_AttributeId",
                schema: _schema?.Schema,
                table: "DynamicStringValues",
                column: "AttributeId",
                principalSchema: _schema?.Schema,
                principalTable: "DynamicStringAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
