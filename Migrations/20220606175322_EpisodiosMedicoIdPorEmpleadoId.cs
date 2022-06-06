using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Historias_Clinicas_D.Migrations
{
    public partial class EpisodiosMedicoIdPorEmpleadoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Epicrisis_Personas_MedicoId",
                table: "Epicrisis");

            migrationBuilder.RenameColumn(
                name: "MedicoId",
                table: "Epicrisis",
                newName: "EmpleadoId");

            migrationBuilder.RenameIndex(
                name: "IX_Epicrisis_MedicoId",
                table: "Epicrisis",
                newName: "IX_Epicrisis_EmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Epicrisis_Personas_EmpleadoId",
                table: "Epicrisis",
                column: "EmpleadoId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Epicrisis_Personas_EmpleadoId",
                table: "Epicrisis");

            migrationBuilder.RenameColumn(
                name: "EmpleadoId",
                table: "Epicrisis",
                newName: "MedicoId");

            migrationBuilder.RenameIndex(
                name: "IX_Epicrisis_EmpleadoId",
                table: "Epicrisis",
                newName: "IX_Epicrisis_MedicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Epicrisis_Personas_MedicoId",
                table: "Epicrisis",
                column: "MedicoId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
