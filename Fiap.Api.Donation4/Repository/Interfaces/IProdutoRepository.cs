using Fiap.Api.Donation4.Models;

namespace Fiap.Api.Donation4.Repository.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IList<ProdutoModel>> FindAllAsync(int pagina, int tamanho);
        Task<IList<ProdutoModel>> FindAllByIdRefAsync(int idProduto, int tamanho);
        Task<IList<ProdutoModel>> FindAllAsync();
        Task<IList<ProdutoModel>> FindByNomeAsync(string nome);
        Task<int> CountAsync();
        Task<ProdutoModel> FindByIdAsync(int id);
        Task<int> InsertAsync(ProdutoModel produtoModel);
        Task UpdateAsync(ProdutoModel produtoModel);
        Task DeleteAsync(ProdutoModel produtoModel);
        Task DeleteAsync(int id);
    }
}
