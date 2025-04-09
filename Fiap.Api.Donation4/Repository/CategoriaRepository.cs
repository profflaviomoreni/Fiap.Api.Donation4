using Fiap.Api.Donation4.Data;
using Fiap.Api.Donation4.Models;
using Fiap.Api.Donation4.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.Donation4.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DataContext _dataContext;

        public CategoriaRepository(DataContext ctx)
        {
            _dataContext = ctx;
        }

        public void Delete(int id)
        {
            var categoria = new CategoriaModel() { CategoriaId = id };

            _dataContext.Categorias.Remove(categoria);
            _dataContext.SaveChanges();
        }

        public IList<CategoriaModel> FindAll()
        {
            return _dataContext.Categorias.AsNoTracking().ToList();
        }

        public CategoriaModel FindById(int id)
        {
            return _dataContext.Categorias.AsNoTracking().FirstOrDefault(c => c.CategoriaId == id);
        }

        public int Insert(CategoriaModel categoriaModel)
        {
            _dataContext.Categorias.Add(categoriaModel);
            _dataContext.SaveChanges();

            return categoriaModel.CategoriaId;
        }

        public void Update(CategoriaModel categoriaModel)
        {
            _dataContext.Categorias.Update(categoriaModel);
            _dataContext.SaveChanges();
        }
    }
}
