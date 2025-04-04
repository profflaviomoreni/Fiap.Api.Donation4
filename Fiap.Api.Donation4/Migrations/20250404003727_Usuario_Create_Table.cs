using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fiap.Api.Donation4.Migrations
{
    /// <inheritdoc />
    public partial class Usuario_Create_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeUsuario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailUsuario = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: false),
                    Regra = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioId);
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "UsuarioId", "EmailUsuario", "NomeUsuario", "Regra", "Senha" },
                values: new object[,]
                {
                    { 1, "admin@admin", "Admin", "admin", "admin123" },
                    { 2, "fmoreni@gmail.com", "Flavio Moreni", "admin", "123456" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_EmailUsuario",
                table: "Usuario",
                column: "EmailUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_EmailUsuario_Senha",
                table: "Usuario",
                columns: new[] { "EmailUsuario", "Senha" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
