using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parqueadero.Migrations
{
    /// <inheritdoc />
    public partial class UserAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Parqueaderos_ParqueaderoId",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "ParqueaderoId",
                table: "Usuarios",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Clave", "Correo", "Nombre", "ParqueaderoId", "Rol" },
                values: new object[] { 1, "$2a$11$WqnxWE3NlieB1JxSQy2ADOSBf/FDZ.gANEGBoNO.yLY4vj0jyWQZe", "superadmin@gmail.com", "Super Usuario", null, 0 });

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Parqueaderos_ParqueaderoId",
                table: "Usuarios",
                column: "ParqueaderoId",
                principalTable: "Parqueaderos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Parqueaderos_ParqueaderoId",
                table: "Usuarios");

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "ParqueaderoId",
                table: "Usuarios",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Parqueaderos_ParqueaderoId",
                table: "Usuarios",
                column: "ParqueaderoId",
                principalTable: "Parqueaderos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
