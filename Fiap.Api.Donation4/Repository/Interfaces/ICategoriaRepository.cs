using Fiap.Api.Donation4.Models;

namespace Fiap.Api.Donation4.Repository.Interfaces
{
    public interface ICategoriaRepository
    {
        public IList<CategoriaModel> FindAll();

        public CategoriaModel FindById(int id);

        public int Insert(CategoriaModel categoriaModel);

        public void Update(CategoriaModel categoriaModel);

        public void Delete(int id);
    }
}
