using Microsoft.EntityFrameworkCore.Migrations;

namespace SGS.Migrations
{
    public partial class notas12e3paramédia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nota",
                table: "AlunoNota",
                newName: "Nota3");

            migrationBuilder.AddColumn<decimal>(
                name: "Nota1",
                table: "AlunoNota",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Nota2",
                table: "AlunoNota",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nota1",
                table: "AlunoNota");

            migrationBuilder.DropColumn(
                name: "Nota2",
                table: "AlunoNota");

            migrationBuilder.RenameColumn(
                name: "Nota3",
                table: "AlunoNota",
                newName: "Nota");
        }
    }
}
