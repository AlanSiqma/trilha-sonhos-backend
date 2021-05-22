
using System;
using System.Collections.Generic;
using Ostium.BeforeIDie.API.Model.Dto;
using Ostium.BeforeIDie.API.Model.Entities.Base;
using Ostium.BeforeIDie.API.Model.Extensions;

namespace Ostium.BeforeIDie.API.Model.Entities
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