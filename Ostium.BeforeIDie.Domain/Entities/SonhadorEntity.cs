using Ostium.BeforeIDie.Domain.Dto;
using Ostium.BeforeIDie.Domain.Entities.Base;
using Ostium.BeforeIDie.Domain.Extensions;
using System.Collections.Generic;

namespace Ostium.BeforeIDie.Domain.Entities
{
    public class SonhadorEntity : BaseEntity
    {
        public SonhadorEntity()
        {
                
        }

        public SonhadorEntity(SonhadorDto sonhadorDto)
        {
            this.Nome = sonhadorDto.Nome;
            this.Email = sonhadorDto.Email;
            this.Senha = sonhadorDto.Senha;
            this.TemaDoUsuario = sonhadorDto.TemaDoUsuario;
        }

        public override string ToString()
        {
            return "SonhadorEntity";
        }
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string TemaDoUsuario { get; set; }

        public IEnumerable<SonhoEntity> ListaSonhos { get; set; }

        public SonhadorEntity AlterarDados(SonhadorDto dto)
        {
            this.Nome = dto.Nome;
            this.Email = dto.Email;
            this.TemaDoUsuario = dto.TemaDoUsuario;
            return this;
        }

        public SonhadorEntity AlterarSenha(string password)
        {
            this.Senha = password.Encrypt();
            return this;
        }
    }
}