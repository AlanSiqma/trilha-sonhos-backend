using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ostium.BeforeIDie.API.Model.Contracts.Repositories;
using Ostium.BeforeIDie.API.Model.Contracts.Respositories;
using Ostium.BeforeIDie.API.Model.Dto;
using Ostium.BeforeIDie.API.Model.Entities;
using Ostium.BeforeIDie.API.Model.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolBoxDeveloper.TemplateEmail.Package.Contracts;
using ToolBoxDeveloper.TemplateEmail.Package.Dto;

namespace Ostium.BeforeIDie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SonhadorController : ControllerBase
    {
        private readonly ISonhadorRepository _sonhadorRepository;
        private readonly ISolicitacaoResetRepository _solicitacaoResetRepository;
        private readonly IEmailProxy _emailProxy;
        private readonly IMapper _mapper;

        public SonhadorController(ISonhadorRepository sonhadorRepository, ISolicitacaoResetRepository solicitacaoResetRepository, IEmailProxy emailProxy, IMapper mapper)
        {
            this._sonhadorRepository = sonhadorRepository;
            this._solicitacaoResetRepository = solicitacaoResetRepository;
            this._emailProxy = emailProxy;
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

                       

                        Dictionary<string, string> payload = new Dictionary<string, string>();
                        payload.Add("USUARIO", sonhador.Nome);
                        payload.Add("URL", "http://localhost:4200/reset-password/" + solicitacao.Id);

                        SendEmailDto dtoSendEmail = new SendEmailDto(
                                                    sender: "trilhasonhos@gmail.com",
                                                    destination: sonhador.Email,
                                                    subject: "RESET DE SENHA",
                                                    payload: payload,
                                                    idTemplate: "60a901543b318194fd678b7c",
                                                    user: "trilhasonhos@gmail.com");

                        var teste = await this._emailProxy.SendEmailFromTemplate(dtoSendEmail);                       

                    }
                }
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
            var entity = await this._solicitacaoResetRepository.Get(dto.Token);
            if (entity.Ativo && entity.DataExpiracaAtiva)
            {
                var sonhador = await this._sonhadorRepository.Get(entity.Usuario);

                entity.Desativar();

                await this._solicitacaoResetRepository.Update(entity.Id, entity);

                result = new SolicitarAlteraracaoSenhaDto() { Email = sonhador.Email };
            }

            return Ok(result);

        }

        [HttpPut("alterar-senha")]
        public async Task<ActionResult> AlterarSenha(SolicitarAlteraracaoSenhaDto dto)
        {
            if (!string.IsNullOrEmpty(dto.Email) && dto.Password.Equals(dto.ConfirmationPassword))
            {
                var entities = await this._sonhadorRepository.Get(x => x.Email.Equals(dto.Email));
                var entity = entities.FirstOrDefault();
                await this._sonhadorRepository.Update(entity.Id, entity.AlterarSenha(dto.Password));
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
