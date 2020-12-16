using Microsoft.AspNetCore.Mvc;
using Ostium.BeforeIDie.API.Model.Dto;
using System;
using System.Threading.Tasks;

namespace Ostium.BeforeIDie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SonhoController : ControllerBase
    {
        public SonhoController()
        {

        }
        [HttpGet]
        public async Task<ActionResult<SonhosDto>> Get()
        {
            return Ok(new SonhosDto());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SonhosDto>> Get(string Id)
        {

            return Ok(new SonhosDto());
        }
        [HttpPost("novo-sonho")]
        public async Task<ActionResult> Post(SonhoDto dto) {

            return Ok(dto);
        }
        [HttpPut("alterar-sonho")]
        public async Task<ActionResult> Put(SonhoDto dto)
        {

            return Ok(dto);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {

            return Ok(id);
        }
    }
}
