using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parqueadero.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDescuentoId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cobros_Descuentos_DescuentoId",
                table: "Cobros");

            migrationBuilder.AlterColumn<int>(
                name: "DescuentoId",
                table: "Cobros",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_UsuarioId",
                table: "Vehiculos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_UsuarioId",
                table: "Reservas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_VehiculoId",
                table: "Reservas",
                column: "VehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ZonaId",
                table: "Reservas",
                column: "ZonaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cobros_Descuentos_DescuentoId",
                table: "Cobros",
                column: "DescuentoId",
                principalTable: "Descuentos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Usuarios_UsuarioId",
                table: "Reservas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Vehiculos_VehiculoId",
                table: "Reservas",
                column: "VehiculoId",
                principalTable: "Vehiculos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Zonas_ZonaId",
                table: "Reservas",
                column: "ZonaId",
                principalTable: "Zonas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculos_Usuarios_UsuarioId",
                table: "Vehiculos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cobros_Descuentos_DescuentoId",
                table: "Cobros");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Usuarios_UsuarioId",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Vehiculos_VehiculoId",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Zonas_ZonaId",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculos_Usuarios_UsuarioId",
                table: "Vehiculos");

            migrationBuilder.DropIndex(
                name: "IX_Vehiculos_UsuarioId",
                table: "Vehiculos");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_UsuarioId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_VehiculoId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_ZonaId",
                table: "Reservas");

            migrationBuilder.AlterColumn<int>(
                name: "DescuentoId",
                table: "Cobros",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cobros_Descuentos_DescuentoId",
                table: "Cobros",
                column: "DescuentoId",
                principalTable: "Descuentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
