using Microsoft.EntityFrameworkCore.Migrations;
using Package.DatabaseManagement;

namespace AioCore.API.Migrations.AioDynamic
{
    public partial class AddDataTypeField : Migration
    {
        private readonly ISchemaDbContext _schema;

        public AddDataTypeField(ISchemaDbContext schema)
        {
            _schema = schema;
        }
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DataType",
                schema: _schema?.Schema,
                table: "DynamicAttributes",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataType",
                schema: _schema?.Schema,
                table: "DynamicAttributes");
        }
    }
}
