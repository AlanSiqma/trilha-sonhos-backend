using System.Threading.Tasks;

namespace Ostium.BeforeIDie.API.Model.Contracts.Services
{
    public interface IEmailService
    {
        Task<bool> SolicitarResetDeSenha(string nome, string idSolicitacao, string email);
    }
}
