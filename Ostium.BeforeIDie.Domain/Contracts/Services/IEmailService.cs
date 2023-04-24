using System.Threading.Tasks;

namespace Ostium.BeforeIDie.Domain.Contracts.Services
{
    public interface IEmailService
    {
        Task<bool> SolicitarResetDeSenha(string nome, string idSolicitacao, string email);
    }
}
