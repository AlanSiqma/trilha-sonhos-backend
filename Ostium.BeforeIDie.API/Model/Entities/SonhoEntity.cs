
using Ostium.BeforeIDie.API.Model.Entities.Base;
using System;

namespace Ostium.BeforeIDie.API.Model.Entities
{
    public class SonhoEntity:BaseEntity{
        public override string ToString()
        {
            return "SonhoEntity";
        }
        public string Sonho { get; set; }

        public string  DescricaoSonho { get; set; }

        public Guid IdStatus { get; set; }

        public Guid IdVisibilidade { get; set; }
        
        public Guid IdSonhador { get; set; }

        /*EF*/

        public StatusSonhoEntity Status { get; set; } 

        public VisibilidadeSonhoEntity Visibilidade  {get;set;}

        public SonhadorEntity Sonhador {get;set;}

    /*EF*/
    }
}

