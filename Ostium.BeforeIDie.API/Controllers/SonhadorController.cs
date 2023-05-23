using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ostium.BeforeIDie.Domain.Contracts.Repositories;
using Ostium.BeforeIDie.Domain.Contracts.Respositories;
using Ostium.BeforeIDie.Domain.Contracts.Services;
using Ostium.BeforeIDie.Domain.Dto;
using Ostium.BeforeIDie.Domain.Entities;
using Ostium.BeforeIDie.Domain.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace Ostium.BeforeIDie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SonhadorController : ControllerBase
    {
        private readonly ISonhadorRepository _sonhadorRepository;
        private readonly ISolicitacaoResetRepository _solicitacaoResetRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly ISonhadorService _sonhadorService;
        public SonhadorController(ISonhadorService sonhadorService,
                                  ISonhadorRepository sonhadorRepository,
                                  ISolicitacaoResetRepository solicitacaoResetRepository,
                                  IEmailService emailService,
                                  IMapper mapper)
        {
            this._sonhadorService = sonhadorService;
            this._sonhadorRepository = sonhadorRepository;
            this._solicitacaoResetRepository = solicitacaoResetRepository;
            this._emailService = emailService;
            this._mapper = mapper;
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
            var result = new SolicitarAlteraracaoSenhaDto() { Email = "" };
            if (await this.TokenValido(dto.Token))
            {
                var solicitacaoEntity = await this._solicitacaoResetRepository.Get(dto.Token);                
                var sonhador = await this._sonhadorRepository.Get(solicitacaoEntity.Usuario);
                result = new SolicitarAlteraracaoSenhaDto() { Email = sonhador.Email };
            }
            return Ok(result);
        }

        private async Task<bool> TokenValido(string token)
        {
            var entity = await this._solicitacaoResetRepository.Get(token);

            if (entity == null)
                return false;

            return entity.Ativo && entity.DataExpiracaAtiva;
        }

        [HttpPut("alterar-senha")]
        public async Task<ActionResult> AlterarSenha(SolicitarAlteraracaoSenhaDto dto)
        {
            if ((await this.TokenValido(dto.Token)) && !string.IsNullOrEmpty(dto.Email) && dto.Password.Equals(dto.ConfirmationPassword))
            {
                var entities = await this._sonhadorRepository.Get(x => x.Email.Equals(dto.Email));
                var entity = entities.FirstOrDefault();
                await this._sonhadorRepository.Update(entity.Id, entity.AlterarSenha(dto.Password));

                var solicitacaoEntity = await this._solicitacaoResetRepository.Get(dto.Token);
                solicitacaoEntity.Desativar();
                await this._solicitacaoResetRepository.Update(solicitacaoEntity.Id, solicitacaoEntity);

                return Ok();
            }
            return BadRequest();
        }


        [HttpPut("alterar-conta")]
        public async Task<ActionResult> Alterar(SonhadorDto dto)
        {
            var entity = (await this._sonhadorRepository.Get(dto.Id));

            await this._sonhadorRepository.Update(entity.Id, entity.AlterarDados(dto));

            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await this._sonhadorRepository.Remove(id);

            return Ok(id);
        }

    }
}
