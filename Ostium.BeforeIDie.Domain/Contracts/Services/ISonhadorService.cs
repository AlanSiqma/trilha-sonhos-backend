using Ostium.BeforeIDie.Domain.Dto;
using Ostium.BeforeIDie.Domain.Entities;
using System.Threading.Tasks;

namespace Ostium.BeforeIDie.Domain.Contracts.Services
{
    public interface ISonhadorService
    {
        Task<SonhadoresDto> Get();
        Task<SonhadorDto> GetById(string id);
        Task<SonhadorEntity> Entrar(LoginDto dto);
        Task<SonhadorDto> Registrar(SonhadorDto dto);
        Task SolicitarAlteracaoSenha(AlteracaoSenhaDto dto);
        Task<SolicitarAlteraracaoSenhaDto> ValidarToken(ValidarTokenDto dto);
        Task AlterarSenha(SolicitarAlteraracaoSenhaDto dto);
        Task<SonhadorDto> Alterar(SonhadorDto dto);
        Task<bool> TokenValido(string token);
        Task<string> Delete(string id);
    }
}
