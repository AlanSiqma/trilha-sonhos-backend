using Ostium.BeforeIDie.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Ostium.BeforeIDie.Domain.Dto
{
    [ExcludeFromCodeCoverageAttribute]
    public class TrilhaDto{
        public TrilhaDto()
        {
        }

        public TrilhaDto(TrilhaEntity x)
        {
            Descricao = x.Descricao;
            Valor = x.Valor;
        }

        public string Descricao { get; set; }

        public bool Valor { get; set; }
    }
}
