using Fiap.Api.Donation4.Repository;

namespace Fiap.Api.Donation4.ViewModel
{
    public class ProdutoPaginacaoResponseViewModel
    {

        public int TotalGeral { get; set; }

        public int TotalPaginas { get; set; }

        public string LinkProximo { get; set; }

        public string LinkAnterior { get; set; }

        public IList<ProdutoResponseViewModel> Produtos { get; set; }

    }
}
