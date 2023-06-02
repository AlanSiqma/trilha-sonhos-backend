using System.Diagnostics.CodeAnalysis;

namespace Ostium.BeforeIDie.Domain.Dto
{
    [ExcludeFromCodeCoverageAttribute]
    public class SolicitarAlteraracaoSenhaDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmationPassword { get; set; }

        public string Token { get; set; }
    }
}
