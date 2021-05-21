using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ostium.BeforeIDie.API.Model.Dto
{
    public class SolicitarAlteraracaoSenhaDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmationPassword { get; set; }
    }
}
