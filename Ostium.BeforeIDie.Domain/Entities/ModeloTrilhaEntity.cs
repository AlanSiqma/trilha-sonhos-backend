using Ostium.BeforeIDie.Domain.Entities.Base;
using System.Collections.Generic;

namespace Ostium.BeforeIDie.Domain.Entities
{
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
