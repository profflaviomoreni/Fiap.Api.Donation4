using Fiap.Api.Donation4.Models;

namespace Fiap.Api.Donation4.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IList<UsuarioModel>> FindAllAsync();
        Task<UsuarioModel> FindByIdAsync(int id);
        Task<int> InsertAsync(UsuarioModel usuarioModel);
        Task UpdateAsync(UsuarioModel usuarioModel);
        Task DeleteAsync(int id);
        Task<UsuarioModel> FindByEmailAndSenhaAsync(string email, string senha);
    }
}
