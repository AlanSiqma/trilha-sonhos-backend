using Ostium.BeforeIDie.Domain.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Ostium.BeforeIDie.Domain.Dto
{
    [ExcludeFromCodeCoverageAttribute]
    public class SonhoDto
    {
        
        public SonhoDto(SonhoEntity entity)
        {
            this.Id = entity.Id;
            this.Sonho = entity.Sonho;
            this.DescricaoSonho = entity.DescricaoSonho;
            this.Status = entity.Status;
            this.Visibilidade = entity.Visibilidade;
            this.IdSonhador = entity.IdSonhador;
            this.Trilhas = new List<TrilhaDto>();
            this.Trilhas = entity.Trilhas.Select(x => new TrilhaDto(x)).ToList();
        }
        public SonhoDto()
        {
            this.Trilhas = new List<TrilhaDto>();
        }
        public string Id { get; set; }

        public string Sonho { get; set; }

        public string DescricaoSonho { get; set; }

        public string Status { get; set; }

        public string Visibilidade { get; set; }

        public string IdSonhador { get; set; }

        public List<TrilhaDto> Trilhas { get; set; }

    }
}
