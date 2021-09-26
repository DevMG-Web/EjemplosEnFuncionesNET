using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreEjemplos.Migrations
{
    public partial class estudiantedetalle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Becado",
                table: "Estudiantes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Carrera",
                table: "Estudiantes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoriaDePago",
                table: "Estudiantes",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Becado",
                table: "Estudiantes");

            migrationBuilder.DropColumn(
                name: "Carrera",
                table: "Estudiantes");

            migrationBuilder.DropColumn(
                name: "CategoriaDePago",
                table: "Estudiantes");
        }
    }
}
