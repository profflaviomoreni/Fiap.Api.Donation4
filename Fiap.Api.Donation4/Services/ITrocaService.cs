using Fiap.Api.Donation4.Models;

namespace Fiap.Api.Donation4.Services
{
    public interface ITrocaService
    {

        public Task<Guid> Trocar(TrocaModel trocaModel);

    }
}
