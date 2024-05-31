
using Ostium.BeforeIDie.Domain.Dto;
using Ostium.BeforeIDie.Domain.Entities.Base;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace Ostium.BeforeIDie.Domain.Entities
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

        public SonhoEntity(SonhoDto dto)
        {
            this.Sonho = dto.Sonho;
            this.DescricaoSonho = dto.DescricaoSonho;
            this.Status = dto.Status;
            this.Visibilidade = dto.Visibilidade;
            this.IdSonhador = dto.IdSonhador; 
            this.Id = dto.Id;
            this.Trilhas = dto.Trilhas.Select(x => new TrilhaEntity(x)).ToList();
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

