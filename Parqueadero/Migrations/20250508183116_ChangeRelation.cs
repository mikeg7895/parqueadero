using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parqueadero.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Parqueaderos_ParqueaderoId",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "ParqueaderoId",
                table: "Usuarios",
                newName: "EmpresaId");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_ParqueaderoId",
                table: "Usuarios",
                newName: "IX_Usuarios_EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Empresas_EmpresaId",
                table: "Usuarios",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Empresas_EmpresaId",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "EmpresaId",
                table: "Usuarios",
                newName: "ParqueaderoId");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_EmpresaId",
                table: "Usuarios",
                newName: "IX_Usuarios_ParqueaderoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Parqueaderos_ParqueaderoId",
                table: "Usuarios",
                column: "ParqueaderoId",
                principalTable: "Parqueaderos",
                principalColumn: "Id");
        }
    }
}
