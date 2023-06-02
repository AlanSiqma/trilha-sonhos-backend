using Ostium.BeforeIDie.Domain.Entities.Base;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Ostium.BeforeIDie.Domain.Entities
{
    [ExcludeFromCodeCoverageAttribute]
    public class ModeloTrilhaEntity: BaseEntity
    {
        public ModeloTrilhaEntity()
        {
            this.Trilhas = new List<TrilhaEntity>();
        }
        public string IdUsuarioRegistro { get; set; }

        public List<TrilhaEntity> Trilhas { get; set; }
    }
}
