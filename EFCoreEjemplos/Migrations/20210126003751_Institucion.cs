using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreEjemplos.Migrations
{
    public partial class Institucion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstitucionId",
                table: "Estudiantes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Instituciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituciones", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_InstitucionId",
                table: "Estudiantes",
                column: "InstitucionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiantes_Instituciones_InstitucionId",
                table: "Estudiantes",
                column: "InstitucionId",
                principalTable: "Instituciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estudiantes_Instituciones_InstitucionId",
                table: "Estudiantes");

            migrationBuilder.DropTable(
                name: "Instituciones");

            migrationBuilder.DropIndex(
                name: "IX_Estudiantes_InstitucionId",
                table: "Estudiantes");

            migrationBuilder.DropColumn(
                name: "InstitucionId",
                table: "Estudiantes");
        }
    }
}
