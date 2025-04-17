using Fiap.Api.Donation4.Models;
using Fiap.Api.Donation4.Repository.Interfaces;

namespace Fiap.Api.Donation4.Services
{
    public class TrocaService : ITrocaService
    {

        private readonly IProdutoRepository _produtoRepository;
        private readonly ITrocaRepository _trocaRepository;

        public TrocaService( IProdutoRepository produtoRepository, ITrocaRepository trocaRepository )
        {
            _produtoRepository = produtoRepository;
            _trocaRepository = trocaRepository;
        }


        public async Task<Guid> Trocar(TrocaModel trocaModel)
        {
            var produto1 = await _produtoRepository.FindByIdAsync(trocaModel.ProdutoIdEscolhido); // Eu quero
            var produto2 = await _produtoRepository.FindByIdAsync(trocaModel.ProdutoIdMeu); // Meu produto

            if (produto1.Disponivel == false)
            {
                throw new Exception("Produto selecionado indisponível");
            }

            if (produto2.Disponivel == false)
            {
                throw new Exception("O seu produto já foi trocado");
            }

            
            if (produto1.UsuarioId == trocaModel.UsuarioId)
            {
                throw new Exception("Esse produto não pode ser escolhido pelo usuário da troca");
            }

            if (produto2.UsuarioId != trocaModel.UsuarioId)
            {
                throw new Exception("Não é possível trocar um produto de outro usuário.");
            }
            


            if ((produto2.Valor / produto1.Valor) < 0.9)
            {
                throw new Exception("O seu produto tem o valor menor que 90% do produto selecionado");
            }

            produto1.Disponivel = false;
            await _produtoRepository.UpdateAsync(produto1);

            produto2.Disponivel = false;
            await _produtoRepository.UpdateAsync(produto2);

            trocaModel.TrocaStatus = TrocaStatus.Iniciado;
            _trocaRepository.Insert(trocaModel);

            return trocaModel.TrocaId;
            
        }
    }
}
