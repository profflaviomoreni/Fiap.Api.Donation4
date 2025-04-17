using System.ComponentModel.DataAnnotations;

namespace Fiap.Api.Donation4.ViewModel
{
    public class ProdutoRequestViewModel
    {

        public int ProdutoId;


        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do produto deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O status de disponibilidade é obrigatório.")]
        public bool Disponivel { get; set; }

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "A sugestão de troca é obrigatória.")]
        [StringLength(200, ErrorMessage = "A sugestão de troca deve ter no máximo 200 caracteres.")]
        public string SugestaoTroca { get; set; }

        [Required(ErrorMessage = "O valor do produto é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "A data de expiração é obrigatória.")]
        public DateTime DataExpiracao { get; set; }

        // Foreign Keys
        [Required(ErrorMessage = "A categoria é obrigatória.")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "O usuário é obrigatório.")]
        public int UsuarioId { get; set; }
    }
}
