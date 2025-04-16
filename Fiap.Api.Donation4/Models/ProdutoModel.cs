namespace Fiap.Api.Donation4.Models
{
    public class ProdutoModel
    {

        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public bool Disponivel { get; set; }
        public string? Descricao { get; set; }
        public string SugestaoTroca { get; set; }
        public double Valor { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime DataExpiracao { get; set; }


        // Foreign Keys
        public int CategoriaId { get; set; }
        public CategoriaModel? Categoria { get; set; }
        public int UsuarioId { get; set; }
        public UsuarioModel? Usuario { get; set; }


    }
}
