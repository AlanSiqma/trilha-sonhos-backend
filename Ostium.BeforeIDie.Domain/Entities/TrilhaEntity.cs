using Ostium.BeforeIDie.Domain.Dto;
using System.Diagnostics.CodeAnalysis;

namespace Ostium.BeforeIDie.Domain.Entities
{
    [ExcludeFromCodeCoverageAttribute]
    public class TrilhaEntity    {
      
        public TrilhaEntity()
        {
        }

        public TrilhaEntity(TrilhaDto x)
        {
            this.Descricao = x.Descricao;
            this.Valor = x.Valor;
        }

        public string Descricao { get; set; }

        public bool Valor { get; set; }
    }
}
