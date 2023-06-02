using System.Diagnostics.CodeAnalysis;

namespace Ostium.BeforeIDie.Domain.Entities
{
    [ExcludeFromCodeCoverageAttribute]
    public class TrilhaEntity    {
        public TrilhaEntity()
        {
        }
        public string Descricao { get; set; }

        public bool Valor { get; set; }
    }
}
