using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Package.DatabaseManagement;

namespace AioCore.API.Migrations.AioDynamic
{
    public partial class AddAuditColumns : Migration
    {
        private readonly ISchemaDbContext _schema;

        public AddAuditColumns(ISchemaDbContext schema)
        {
            _schema = schema;
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Created",
                schema: _schema?.Schema,
                table: "DynamicEntities",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: _schema?.Schema,
                table: "DynamicStringValues",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                schema: _schema?.Schema,
                table: "DynamicStringValues",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: _schema?.Schema,
                table: "DynamicStringValues",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                schema: _schema?.Schema,
                table: "DynamicStringValues",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: _schema?.Schema,
                table: "DynamicIntegerValues",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                schema: _schema?.Schema,
                table: "DynamicIntegerValues",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: _schema?.Schema,
                table: "DynamicIntegerValues",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                schema: _schema?.Schema,
                table: "DynamicIntegerValues",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: _schema?.Schema,
                table: "DynamicGuidValues",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                schema: _schema?.Schema,
                table: "DynamicGuidValues",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: _schema?.Schema,
                table: "DynamicGuidValues",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                schema: _schema?.Schema,
                table: "DynamicGuidValues",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: _schema?.Schema,
                table: "DynamicFloatValues",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                schema: _schema?.Schema,
                table: "DynamicFloatValues",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: _schema?.Schema,
                table: "DynamicFloatValues",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                schema: _schema?.Schema,
                table: "DynamicFloatValues",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: _schema?.Schema,
                table: "DynamicEntities",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: _schema?.Schema,
                table: "DynamicEntities",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                schema: _schema?.Schema,
                table: "DynamicEntities",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: _schema?.Schema,
                table: "DynamicDateValues",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                schema: _schema?.Schema,
                table: "DynamicDateValues",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: _schema?.Schema,
                table: "DynamicDateValues",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                schema: _schema?.Schema,
                table: "DynamicDateValues",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: _schema?.Schema,
                table: "DynamicAttributes",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                schema: _schema?.Schema,
                table: "DynamicAttributes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: _schema?.Schema,
                table: "DynamicAttributes",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                schema: _schema?.Schema,
                table: "DynamicAttributes",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: _schema?.Schema,
                table: "DynamicStringValues");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: _schema?.Schema,
                table: "DynamicStringValues");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: _schema?.Schema,
                table: "DynamicStringValues");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                schema: _schema?.Schema,
                table: "DynamicStringValues");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: _schema?.Schema,
                table: "DynamicIntegerValues");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: _schema?.Schema,
                table: "DynamicIntegerValues");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: _schema?.Schema,
                table: "DynamicIntegerValues");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                schema: _schema?.Schema,
                table: "DynamicIntegerValues");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: _schema?.Schema,
                table: "DynamicGuidValues");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: _schema?.Schema,
                table: "DynamicGuidValues");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: _schema?.Schema,
                table: "DynamicGuidValues");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                schema: _schema?.Schema,
                table: "DynamicGuidValues");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: _schema?.Schema,
                table: "DynamicFloatValues");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: _schema?.Schema,
                table: "DynamicFloatValues");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: _schema?.Schema,
                table: "DynamicFloatValues");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                schema: _schema?.Schema,
                table: "DynamicFloatValues");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: _schema?.Schema,
                table: "DynamicEntities");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: _schema?.Schema,
                table: "DynamicEntities");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                schema: _schema?.Schema,
                table: "DynamicEntities");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: _schema?.Schema,
                table: "DynamicDateValues");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: _schema?.Schema,
                table: "DynamicDateValues");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: _schema?.Schema,
                table: "DynamicDateValues");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                schema: _schema?.Schema,
                table: "DynamicDateValues");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: _schema?.Schema,
                table: "DynamicAttributes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: _schema?.Schema,
                table: "DynamicAttributes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: _schema?.Schema,
                table: "DynamicAttributes");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                schema: _schema?.Schema,
                table: "DynamicAttributes");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                schema: _schema?.Schema,
                table: "DynamicEntities",
                newName: "Created");
        }
    }
}
