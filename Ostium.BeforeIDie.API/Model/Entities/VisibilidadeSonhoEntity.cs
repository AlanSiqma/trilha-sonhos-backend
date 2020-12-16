
using Ostium.BeforeIDie.API.Model.Entities.Base;

namespace Ostium.BeforeIDie.API.Model.Entities
{
    public class VisibilidadeSonhoEntity : BaseEntity
    {
        public override string ToString()
        {
            return "VisibilidadeSonhoEntity";
        }
        public string Visibilidade { get; set; }
    }
}