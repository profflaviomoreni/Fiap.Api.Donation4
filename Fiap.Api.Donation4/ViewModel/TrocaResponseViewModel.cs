using Fiap.Api.Donation4.Models;

namespace Fiap.Api.Donation4.ViewModel
{
    public class TrocaResponseViewModel
    {

        public Guid TrocaId { get; set; }

        public TrocaStatus TrocaStatus { get; set; }

        public DateTime DataCriacao { get; set; }

        public int ProdutoIdMeu { get; set; }

        public string NomeProdutoMeu { get; set; } // Nome do Produto do usuário

        public int ProdutoIdEscolhido { get; set; }

        public string NomeProdutoEscolhido { get; set; } // Nome do Produto escolhido


    }
}
