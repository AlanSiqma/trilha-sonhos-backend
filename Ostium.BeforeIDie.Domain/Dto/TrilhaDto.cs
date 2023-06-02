using Ostium.BeforeIDie.Domain.Entities.Base;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Ostium.BeforeIDie.Domain.Dto
{
    [ExcludeFromCodeCoverageAttribute]
    public class TrilhaDto{
        public TrilhaDto()
        {
        }
        public string Descricao { get; set; }

        public bool Valor { get; set; }
    }
}
