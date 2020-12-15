
using System.Collections.Generic;

using Ostium.BeforeIDie.API.Model.Entities.Base;

namespace Ostium.BeforeIDie.API.Model.Entities
{
    public class SonhadorEntity:BaseEntity{

        public string Nome { get; set; }

        public string Email { get; set; }   

        public string Senha { get; set; }

        public IEnumerable<SonhoEntity> ListaSonhos { get; set; }

    }
}