using System.ComponentModel.DataAnnotations;

namespace Fiap.Api.Donation4.Models
{
    public enum TrocaStatus
    {
        Iniciado = 1,
        Analisado = 2,
        Finalizado = 3,
        Revertido = 4
    }

    public class TrocaModel
    {

        [Key]
        public Guid TrocaId { get; set; } = Guid.NewGuid();

        public TrocaStatus TrocaStatus { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public int ProdutoIdMeu { get; set; }

        public ProdutoModel ProdutoMeu { get; set; }

        public int ProdutoIdEscolhido { get; set; }

        public ProdutoModel ProdutoEscolhido { get; set; }

        public int UsuarioId { get; set; }

        public UsuarioModel Usuario { get; set; }


    }
}
