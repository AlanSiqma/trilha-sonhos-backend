﻿using System;
using System.Collections.Generic;

namespace Ostium.BeforeIDie.Domain.Dto
{
    public class SonhosDto
    {
        public SonhosDto()
        {
            this.Sonhos = new List<SonhoDto>();
        }
        public IEnumerable<SonhoDto> Sonhos { get; set; }

        public string IdSonhoDestaque { get; set; }

        public SonhosDto AddSonhos(List<SonhoDto> map)
        {
            this.Sonhos = map;

            return this;
        }

        public void SetSonhoDestaque(string id)
        {
            this.IdSonhoDestaque = id;
        }
    }
}
