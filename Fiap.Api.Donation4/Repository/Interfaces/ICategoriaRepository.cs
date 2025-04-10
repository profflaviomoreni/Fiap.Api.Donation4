using Fiap.Api.Donation4.Models;

namespace Fiap.Api.Donation4.Repository.Interfaces
{
    public interface ICategoriaRepository
    {
        public Task<IList<CategoriaModel>> FindAllAsync();

        public Task<CategoriaModel> FindByIdAsync(int id);

        public Task<int> InsertAsync(CategoriaModel categoriaModel);

        public Task UpdateAsync(CategoriaModel categoriaModel);

        public Task DeleteAsync(int id);
    }
}
