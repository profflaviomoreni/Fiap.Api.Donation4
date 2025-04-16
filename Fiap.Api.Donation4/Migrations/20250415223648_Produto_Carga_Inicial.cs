using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Api.Donation4.Migrations
{
    /// <inheritdoc />
    public partial class Produto_Carga_Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        INSERT INTO Produto (Nome, Disponivel, Descricao, SugestaoTroca, Valor, DataCadastro, DataExpiracao, CategoriaId, UsuarioId) VALUES
        ('Produto 1', 1, 'Descricao do produto 1', 'Troca por livro', 99.99, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 1, 1),
        ('Produto 2', 0, 'Descricao do produto 2', 'Troca por ferramenta', 49.99, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 2, 1),
        ('Produto 3', 1, 'Descricao do produto 3', 'Troca por bicicleta', 199.99, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 3, 2),
        ('Produto 4', 1, 'Descricao do produto 4', 'Troca por livro', 75.50, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 1, 2),
        ('Produto 5', 0, 'Descricao do produto 5', 'Troca por fone de ouvido', 89.90, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 2, 1),
        ('Produto 6', 1, 'Descricao do produto 6', 'Troca por monitor', 150.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 3, 1),
        ('Produto 7', 1, 'Descricao do produto 7', 'Troca por teclado', 45.75, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 1, 1),
        ('Produto 8', 1, 'Descricao do produto 8', 'Troca por mouse', 30.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 2, 2),
        ('Produto 9', 1, 'Descricao do produto 9', 'Troca por cadeira gamer', 250.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 3, 2),
        ('Produto 10', 1, 'Descricao do produto 10', 'Troca por mesa de escritório', 200.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 1, 1),
        ('Produto 11', 0, 'Descricao do produto 11', 'Troca por headphones', 70.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 2, 1),
        ('Produto 12', 1, 'Descricao do produto 12', 'Troca por monitor 4K', 400.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 3, 2),
        ('Produto 13', 1, 'Descricao do produto 13', 'Troca por mesa de jantar', 180.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 1, 2),
        ('Produto 14', 0, 'Descricao do produto 14', 'Troca por mochila', 40.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 2, 1),
        ('Produto 15', 1, 'Descricao do produto 15', 'Troca por câmera digital', 299.99, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 3, 1),
        ('Produto 16', 1, 'Descricao do produto 16', 'Troca por impressora', 120.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 1, 1),
        ('Produto 17', 1, 'Descricao do produto 17', 'Troca por smart TV', 600.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 2, 2),
        ('Produto 18', 1, 'Descricao do produto 18', 'Troca por smartphone', 500.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 3, 2),
        ('Produto 19', 0, 'Descricao do produto 19', 'Troca por tablet', 250.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 1, 1),
        ('Produto 20', 1, 'Descricao do produto 20', 'Troca por projetor', 350.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 2, 1),
        ('Produto 21', 1, 'Descricao do produto 21', 'Troca por DVD player', 60.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 3, 2),
        ('Produto 22', 0, 'Descricao do produto 22', 'Troca por blu-ray player', 80.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 1, 1),
        ('Produto 23', 1, 'Descricao do produto 23', 'Troca por fogão', 400.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 2, 2),
        ('Produto 24', 1, 'Descricao do produto 24', 'Troca por micro-ondas', 150.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 3, 1),
        ('Produto 25', 1, 'Descricao do produto 25', 'Troca por geladeira', 900.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 1, 2),
        ('Produto 26', 0, 'Descricao do produto 26', 'Troca por máquina de lavar', 600.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 2, 1),
        ('Produto 27', 1, 'Descricao do produto 27', 'Troca por aspirador de pó', 100.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 3, 2),
        ('Produto 28', 1, 'Descricao do produto 28', 'Troca por liquidificador', 50.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 1, 1),
        ('Produto 29', 1, 'Descricao do produto 29', 'Troca por ventilador', 60.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 2, 2),
        ('Produto 30', 1, 'Descricao do produto 30', 'Troca por aquecedor', 120.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 3, 1),
        ('Produto 31', 0, 'Descricao do produto 31', 'Troca por lava-louças', 700.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 1, 2),
        ('Produto 32', 1, 'Descricao do produto 32', 'Troca por ferro de passar', 35.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 2, 1),
        ('Produto 33', 1, 'Descricao do produto 33', 'Troca por relógio digital', 150.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 3, 1),
        ('Produto 34', 0, 'Descricao do produto 34', 'Troca por calculadora gráfica', 80.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 1, 2),
        ('Produto 35', 1, 'Descricao do produto 35', 'Troca por videogame', 450.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 2, 1),
        ('Produto 36', 1, 'Descricao do produto 36', 'Troca por tablet gráfico', 300.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 3, 2),
        ('Produto 37', 1, 'Descricao do produto 37', 'Troca por câmera de segurança', 150.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 1, 2),
        ('Produto 38', 1, 'Descricao do produto 38', 'Troca por painel solar', 1000.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 2, 1),
        ('Produto 39', 0, 'Descricao do produto 39', 'Troca por drone', 800.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 3, 1),
        ('Produto 40', 1, 'Descricao do produto 40', 'Troca por smartphone', 500.00, GETDATE(), DATEADD(MONTH, 6, GETDATE()), 1, 1)
    ");
        }
        

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Produtos WHERE Nome LIKE 'Produto %'");
        }
    }
}
