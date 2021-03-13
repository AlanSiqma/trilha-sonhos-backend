﻿using Ostium.BeforeIDie.API.Model.Entities;
using Ostium.BeforeIDie.API.Model.Entities.Base;
using System.Collections.Generic;

namespace Ostium.BeforeIDie.API.Model.Dto
{
    public class ModeloTrilhaDto 
    {
        public ModeloTrilhaDto()
        {
            this.Trilhas = new List<TrilhaDto>();
        }
        public string Id { get; set; }

        public string IdUsuarioRegistro { get; set; }

        public List<TrilhaDto> Trilhas { get; set; }
    }
}