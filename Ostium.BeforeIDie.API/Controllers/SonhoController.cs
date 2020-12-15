using Microsoft.AspNetCore.Mvc;
using Ostium.BeforeIDie.API.Model.Dto;
using System;
using System.Threading.Tasks;

namespace Ostium.BeforeIDie.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SonhoController : ControllerBase
    {
        public SonhoController()
        {

        }
        [HttpGet]
        public async Task<ActionResult<SonhosDto>> Get()
        {
            return new SonhosDto();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SonhosDto>> Get(Guid Id)
        {
            return new SonhosDto();
        }
        [HttpPost("novo-sonho")]
        public async Task<ActionResult> Post(SonhoDto dto) {

            return Ok();
        }
        [HttpPut("alterar-sonho")]
        public async Task<ActionResult> Put(SonhoDto dto)
        {

            return Ok();
        }
    }
}
