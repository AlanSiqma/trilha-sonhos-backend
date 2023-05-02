using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ostium.BeforeIDie.Domain.Dto
{
    public class SonhadoresDto
    {
        public SonhadoresDto()
        {
            this.Sonhadores = new List<SonhadorDto>();
        }
        public IEnumerable<SonhadorDto> Sonhadores{ get; set; }

        public SonhadoresDto AddSonhadores(List<SonhadorDto> map)
        {
            this.Sonhadores = map;
            return this;
        }
    }
}
