
using System;
using System.Collections.Generic;
using Ostium.BeforeIDie.Domain.Dto;
using Ostium.BeforeIDie.Domain.Entities.Base;
using Ostium.BeforeIDie.Domain.Extensions;

namespace Ostium.BeforeIDie.Domain.Entities
{
    public class SonhadorEntity : BaseEntity
    {

        public override string ToString()
        {
            return "SonhadorEntity";
        }
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string TemaDoUsuario { get; set; }

        public IEnumerable<SonhoEntity> ListaSonhos { get; set; }

        internal SonhadorEntity AlterarDados(SonhadorDto dto)
        {
            this.Nome = dto.Nome;
            this.Email = dto.Email;
            this.TemaDoUsuario = dto.TemaDoUsuario;
            return this;
        }

        internal SonhadorEntity AlterarSenha(string password)
        {
            this.Senha = password.Encrypt();
            return this;
        }
    }
}