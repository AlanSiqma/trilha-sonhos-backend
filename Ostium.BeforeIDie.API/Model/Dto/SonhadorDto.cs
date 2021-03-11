using Ostium.BeforeIDie.API.Model.Entities;
using Ostium.BeforeIDie.API.Model.Extensions;

namespace Ostium.BeforeIDie.API.Model.Dto
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
        }

        public string Id { get; set; }
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        internal SonhadorDto EncryptSenha()
        {
            this.Senha = this.Senha.Encrypt();
            return this;
        }
    }
}
