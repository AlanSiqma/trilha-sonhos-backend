using Microsoft.AspNetCore.Mvc;
using Ostium.BeforeIDie.Domain.Contracts.Services;
using Ostium.BeforeIDie.Domain.Dto;
using System.Threading.Tasks;

namespace Ostium.BeforeIDie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SonhadorController : ControllerBase
    {
        private readonly ISonhadorService _sonhadorService;
        public SonhadorController(ISonhadorService sonhadorService)
        {
            this._sonhadorService = sonhadorService;
        }
        [HttpGet]
        public async Task<ActionResult<SonhadoresDto>> Get()
        {
            return Ok(await this._sonhadorService.Get());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SonhadorDto>> Get(string id)
        {
            return Ok(await this._sonhadorService.GetById(id));
        }

        [HttpPost("entrar")]
        public async Task<ActionResult> Entrar(LoginDto dto)
        {
            return Ok(await this._sonhadorService.Entrar(dto));
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(SonhadorDto dto)
        {
            return Ok(await this._sonhadorService.Registrar(dto));
        }

        [HttpPut("solicitar-alteracao-senha")]
        public async Task<ActionResult> SolicitarAlteracaoSenha(AlteracaoSenhaDto dto)
        {
            try
            {
                await this._sonhadorService.SolicitarAlteracaoSenha(dto);
                return Ok();
            }
            catch (System.Exception ex)
            {
                var teste = ex.Message;
                return BadRequest();
            }
        }

        [HttpPut("validar-token")]
        public async Task<ActionResult> ValidarToken(ValidarTokenDto dto)
        {
            return Ok(await this._sonhadorService.ValidarToken(dto));
        }

        [HttpPut("alterar-senha")]
        public async Task<ActionResult> AlterarSenha(SolicitarAlteraracaoSenhaDto dto)
        {
            try
            {
                await this._sonhadorService.AlterarSenha(dto);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("alterar-conta")]
        public async Task<ActionResult> Alterar(SonhadorDto dto)
        {
            await this._sonhadorService.Alterar(dto);
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await this._sonhadorService.Delete(id);
            return Ok(id);
        }
    }
}