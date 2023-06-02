
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Ostium.BeforeIDie.Domain.Dto
{
    [ExcludeFromCodeCoverageAttribute]
    public class ModeloTrilhaDto 
    {
        public ModeloTrilhaDto()
        {
            this.Trilhas = new List<TrilhaDto>();
        }
        public string Id { get; set; }

        public string IdUsuarioRegistro { get; set; }

        public List<TrilhaDto> Trilhas { get; set; }
    }
}
