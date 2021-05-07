﻿using Ostium.BeforeIDie.API.Model.Entities;
using System.Collections.Generic;

namespace Ostium.BeforeIDie.API.Model.Dto
{
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
