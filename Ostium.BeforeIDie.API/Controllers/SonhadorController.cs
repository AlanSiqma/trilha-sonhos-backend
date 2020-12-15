using Microsoft.AspNetCore.Mvc;
using Ostium.BeforeIDie.API.Model.Dto;
using System;
using System.Threading.Tasks;

namespace Ostium.BeforeIDie.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SonhadorController : ControllerBase
    {
        public SonhadorController()
        {

        }
        [HttpGet]
        public async Task<ActionResult<SonhadoresDto>> Get()
        {
            return new SonhadoresDto();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SonhadorDto>> Get(Guid Id)
        {
            return new SonhadorDto();
        }
        [HttpPost("entrar")]
        public async Task<ActionResult> Entrar(LoginDto dto)
        {

            return Ok();
        }
        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(RegisterUserDto registerUser)
        {
          
            return Ok(registerUser);
        }

    }
}
