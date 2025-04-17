using System.ComponentModel.DataAnnotations;

namespace Fiap.Api.Donation4.ViewModel
{
    public class TrocaRequestViewModel
    {
        [Required(ErrorMessage = "O ID do produto do usuário é obrigatório.")]
        public int ProdutoIdMeu { get; set; }

        [Required(ErrorMessage = "O ID do produto escolhido é obrigatório.")]
        public int ProdutoIdEscolhido { get; set; }
    }
}
