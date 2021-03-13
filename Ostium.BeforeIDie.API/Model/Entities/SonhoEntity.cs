
using Ostium.BeforeIDie.API.Model.Entities.Base;
using System.Collections.Generic;

namespace Ostium.BeforeIDie.API.Model.Entities
{
    public class SonhoEntity:BaseEntity{
        public override string ToString()
        {
            return "SonhoEntity";
        }
        public SonhoEntity()
        {
            this.Trilhas = new List<TrilhaEntity>();
        }
        public string Sonho { get; set; }

        public string  DescricaoSonho { get; set; }

        public string Status { get; set; }

        public string Visibilidade { get; set; }
        
        public string IdSonhador { get; set; }

        public SonhadorEntity Sonhador {get;set;}

        public List<TrilhaEntity> Trilhas { get; set; }
    }
}

