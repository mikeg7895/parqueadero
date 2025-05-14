using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parqueadero.Migrations
{
    /// <inheritdoc />
    public partial class UserNullableParqueadero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "Clave",
                value: "$2a$11$LQv3c1yqBWVHxkd0LHAkCOYz6TtxMQJqhN8/LewdBAQHxKJ5QKz6e");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "Clave",
                value: "$2a$11$WqnxWE3NlieB1JxSQy2ADOSBf/FDZ.gANEGBoNO.yLY4vj0jyWQZe");
        }
    }
}
