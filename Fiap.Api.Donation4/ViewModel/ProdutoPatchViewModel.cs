using System.ComponentModel.DataAnnotations;

namespace Fiap.Api.Donation4.ViewModel
{
    public class ProdutoPatchViewModel
    {
        [StringLength(100, ErrorMessage = "O nome do produto deve ter no máximo 100 caracteres.")]
        public string? Nome { get; set; }

        public bool? Disponivel { get; set; }

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string? Descricao { get; set; }

        [StringLength(200, ErrorMessage = "A sugestão de troca deve ter no máximo 200 caracteres.")]
        public string? SugestaoTroca { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public double? Valor { get; set; }

        public DateTime? DataExpiracao { get; set; }

        // Foreign Keys
        public int? CategoriaId { get; set; }
        public int? UsuarioId { get; set; }
    }
}
