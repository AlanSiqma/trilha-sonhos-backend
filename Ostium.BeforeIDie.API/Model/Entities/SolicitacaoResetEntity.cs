using Ostium.BeforeIDie.API.Model.Entities.Base;
using System;

namespace Ostium.BeforeIDie.API.Model.Entities
{
    public class SolicitacaoResetEntity : BaseEntity
    {
        public SolicitacaoResetEntity()
        {

        }
        public override string ToString()
        {
            return "SolicitacaoResetEntity";
        }
        public SolicitacaoResetEntity(string usuario)
        {
            this.Usuario = usuario;
            this.DateSolicitacao = DateTime.UtcNow;
            this.DataExpiracao = this.DateSolicitacao.AddHours(5);
            this.Ativo = true;
        }
        public string Usuario { get; set; }

        public DateTime DateSolicitacao { get; set; }

        public DateTime DataExpiracao { get; set; }

        public bool Ativo { get; set; }

        public bool DataExpiracaAtiva
        {
            get
            {
                if (this.DataExpiracao < DateTime.UtcNow)
                    return false;

                return true;
            }
        }

        public void Desativar()
        {
            this.Ativo = false;
        }

    }
}
