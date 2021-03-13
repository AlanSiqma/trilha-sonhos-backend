using Ostium.BeforeIDie.API.Model.Entities.Base;
using System.Collections.Generic;

namespace Ostium.BeforeIDie.API.Model.Entities
{
    public class TrilhaEntity    {
        public TrilhaEntity()
        {
            this.PassosTrilha = new List<string>();
        }
        public string NomeTrilha { get; set; }

        public List<string> PassosTrilha { get; set; }
    }
}
