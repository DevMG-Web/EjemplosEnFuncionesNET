using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreEjemplos.Migrations
{
    public partial class EstudianteAgregoEdad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "apellido",
                table: "Estudiantes",
                newName: "Apellido");

            migrationBuilder.AddColumn<int>(
                name: "Edad",
                table: "Estudiantes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Edad",
                table: "Estudiantes");

            migrationBuilder.RenameColumn(
                name: "Apellido",
                table: "Estudiantes",
                newName: "apellido");
        }
    }
}
