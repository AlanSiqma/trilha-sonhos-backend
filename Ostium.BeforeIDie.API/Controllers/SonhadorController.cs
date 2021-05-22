using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ostium.BeforeIDie.API.Model.Contracts.Repositories;
using Ostium.BeforeIDie.API.Model.Contracts.Respositories;
using Ostium.BeforeIDie.API.Model.Contracts.Services;
using Ostium.BeforeIDie.API.Model.Dto;
using Ostium.BeforeIDie.API.Model.Entities;
using Ostium.BeforeIDie.API.Model.Extensions;
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

        public SonhadorController(ISonhadorRepository sonhadorRepository,
                                  ISolicitacaoResetRepository solicitacaoResetRepository,
                                  IEmailService emailService,
                                  IMapper mapper)
        {
            this._sonhadorRepository = sonhadorRepository;
            this._solicitacaoResetRepository = solicitacaoResetRepository;
            this._emailService = emailService;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<SonhadoresDto>> Get()
        {
            var map = (await this._sonhadorRepository.Get()).Select(x => new SonhadorDto(x)).ToList();

            return Ok(new SonhadoresDto().AddSonhadores(map));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SonhadorDto>> Get(string id)
        {
            var entity = await this._sonhadorRepository.Get(id);

            var map = this._mapper.Map<SonhadorDto>(entity);

            if (map != null)
                map.Senha = string.Empty;

            return Ok(map);
        }

        [HttpPost("entrar")]
        public async Task<ActionResult> Entrar(LoginDto dto)
        {
            SonhadorEntity usuario = (await this._sonhadorRepository.Get(x =>
                                                             x.Email.Equals(dto.Email) &&
                                                             x.Senha.Equals(dto.Password.Encrypt()))).FirstOrDefault();

            if (usuario != null)
                usuario.Senha = string.Empty;

            return Ok(usuario);
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(SonhadorDto dto)
        {
            if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Senha) || string.IsNullOrEmpty(dto.Nome))
                return BadRequest();

            var map = this._mapper.Map<SonhadorEntity>(dto.EncryptSenha());

            await this._sonhadorRepository.Create(map);

            return Ok(dto);
        }

        [HttpPut("solicitar-alteracao-senha")]
        public async Task<ActionResult> SolicitarAlteracaoSenha(AlteracaoSenhaDto dto)
        {
            try
            {
                if (!string.IsNullOrEmpty(dto.Email))
                {
                    var entities = await this._sonhadorRepository.Get(x => x.Email.Equals(dto.Email));

                    if (entities.Count > 0)
                    {
                        var sonhador = entities.FirstOrDefault();

                        var solicitacoes = await this._solicitacaoResetRepository.Get(x => x.Usuario.Equals(sonhador.Id) && x.Ativo);
                        var solicitacao = new SolicitacaoResetEntity(sonhador.Id);

                        if (solicitacoes.Where(x => x.DataExpiracaAtiva).ToList().Count == 0)
                            await this._solicitacaoResetRepository.Create(solicitacao);
                        else
                            solicitacao = solicitacoes.FirstOrDefault();

                        if (await this._emailService.SolicitarResetDeSenha(nome: sonhador.Nome, idSolicitacao: solicitacao.Id, email: sonhador.Email))
                            return Ok();
                        else
                            BadRequest();
                    }
                }
                return BadRequest();
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
