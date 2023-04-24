using Ostium.BeforeIDie.Domain.Entities;
using Ostium.BeforeIDie.Domain.Extensions;

namespace Ostium.BeforeIDie.Domain.Dto
{
    public class SonhadorDto
    {
        public SonhadorDto()
        {

        }
        public SonhadorDto(SonhadorEntity entity)
        {
            this.Id = entity.Id;
            this.Nome = entity.Nome;
            this.Email = entity.Email;
            this.TemaDoUsuario = entity.TemaDoUsuario;
        }

        public string Id { get; set; }
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string TemaDoUsuario { get; set; }

        internal SonhadorDto EncryptSenha()
        {
            this.Senha = this.Senha.Encrypt();
            return this;
        }
    }
}
