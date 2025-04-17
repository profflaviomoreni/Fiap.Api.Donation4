using Fiap.Api.Donation4.Models;

namespace Fiap.Api.Donation4.Repository.Interfaces
{
    public interface ITrocaRepository
    {

        public Task<Guid> Insert(TrocaModel trocaModel);

        public Task<TrocaModel> FindById(Guid id);

    }
}
