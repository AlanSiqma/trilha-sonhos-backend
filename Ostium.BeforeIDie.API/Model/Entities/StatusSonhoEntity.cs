
using Ostium.BeforeIDie.API.Model.Entities.Base;
namespace Ostium.BeforeIDie.API.Model.Entities

{
    public class StatusSonhoEntity: BaseEntity{
        public override string ToString()
        {
            return "StatusSonhoEntity";
        }
        public string Status { get; set; }
    }

}