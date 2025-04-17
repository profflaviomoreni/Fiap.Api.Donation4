using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Api.Donation4.Migrations
{
    /// <inheritdoc />
    public partial class Troca_Create_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Troca",
                columns: table => new
                {
                    TrocaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrocaStatus = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProdutoIdMeu = table.Column<int>(type: "int", nullable: false),
                    ProdutoIdEscolhido = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Troca", x => x.TrocaId);
                    table.ForeignKey(
                        name: "FK_Troca_Produto_ProdutoIdEscolhido",
                        column: x => x.ProdutoIdEscolhido,
                        principalTable: "Produto",
                        principalColumn: "ProdutoId");
                    table.ForeignKey(
                        name: "FK_Troca_Produto_ProdutoIdMeu",
                        column: x => x.ProdutoIdMeu,
                        principalTable: "Produto",
                        principalColumn: "ProdutoId");
                    table.ForeignKey(
                        name: "FK_Troca_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Troca_ProdutoIdEscolhido",
                table: "Troca",
                column: "ProdutoIdEscolhido");

            migrationBuilder.CreateIndex(
                name: "IX_Troca_ProdutoIdMeu",
                table: "Troca",
                column: "ProdutoIdMeu");

            migrationBuilder.CreateIndex(
                name: "IX_Troca_UsuarioId",
                table: "Troca",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Troca");
        }
    }
}
