using AutoMapper;
using Ostium.BeforeIDie.Domain.Contracts.Repositories;
using Ostium.BeforeIDie.Domain.Contracts.Respositories;
using Ostium.BeforeIDie.Domain.Contracts.Services;
using Ostium.BeforeIDie.Domain.Dto;
using Ostium.BeforeIDie.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using Ostium.BeforeIDie.Domain.Extensions;
using System;

namespace Ostium.BeforeIDie.Services
{
    public class SonhadorService : ISonhadorService
    {
        private readonly ISonhadorRepository _sonhadorRepository;
        private readonly ISolicitacaoResetRepository _solicitacaoResetRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        public SonhadorService(ISonhadorRepository sonhadorRepository,
                                  ISolicitacaoResetRepository solicitacaoResetRepository,
                                  IEmailService emailService,
                                  IMapper mapper)
        {
            this._sonhadorRepository = sonhadorRepository;
            this._solicitacaoResetRepository = solicitacaoResetRepository;
            this._emailService = emailService;
            this._mapper = mapper;
        }

        public async Task<SonhadoresDto> Get()
        {
            var map = await this._sonhadorRepository.Get();

            var listSonhadorDto = map.Select(x => new SonhadorDto(x)).ToList();

            return new SonhadoresDto().AddSonhadores(listSonhadorDto);
        }
        public async Task<SonhadorDto> GetById(string id)
        {
            var entity = await this._sonhadorRepository.Get(id);

            var map = this._mapper.Map<SonhadorDto>(entity);

            if (map != null)
                map.Senha = string.Empty;

            return map;
        }
        public async Task<SonhadorEntity> Entrar(LoginDto dto)
        {
            SonhadorEntity usuario = (await this._sonhadorRepository.Get(x =>
                                                             x.Email.Equals(dto.Email) &&
                                                             x.Senha.Equals(dto.Password.Encrypt()))).FirstOrDefault();

            if (usuario != null)
                usuario.Senha = string.Empty;

            return usuario;
        }
        public async Task<SonhadorDto> Registrar(SonhadorDto dto)
        {
            if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Senha) || string.IsNullOrEmpty(dto.Nome))
                throw new ArgumentException("Emil, Senha e nome são obrigatorios");

            var map = this._mapper.Map<SonhadorEntity>(dto.EncryptSenha());

            await this._sonhadorRepository.Create(map);

            return dto;
        }
        public async Task SolicitarAlteracaoSenha(AlteracaoSenhaDto dto)
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

                    await this._emailService.SolicitarResetDeSenha(nome: sonhador.Nome, idSolicitacao: solicitacao.Id, email: sonhador.Email);
                }
            }
        }
        public async Task<SolicitarAlteraracaoSenhaDto> ValidarToken(ValidarTokenDto dto)
        {
            var result = new SolicitarAlteraracaoSenhaDto() { Email = "" };
            if (await this.TokenValido(dto.Token))
            {
                var solicitacaoEntity = await this._solicitacaoResetRepository.Get(dto.Token);
                var sonhador = await this._sonhadorRepository.Get(solicitacaoEntity.Usuario);
                result = new SolicitarAlteraracaoSenhaDto() { Email = sonhador.Email };
            }
            return result;
        }
        public async Task<bool> TokenValido(string token)
        {
            var entity = await this._solicitacaoResetRepository.Get(token);

            if (entity == null)
                return false;

            return entity.Ativo && entity.DataExpiracaAtiva;
        }
        public async Task AlterarSenha(SolicitarAlteraracaoSenhaDto dto)
        {
            if ((await this.TokenValido(dto.Token)) && !string.IsNullOrEmpty(dto.Email) && dto.Password.Equals(dto.ConfirmationPassword))
            {
                var entities = await this._sonhadorRepository.Get(x => x.Email.Equals(dto.Email));
                var entity = entities.FirstOrDefault();
                await this._sonhadorRepository.Update(entity.Id, entity.AlterarSenha(dto.Password));

                var solicitacaoEntity = await this._solicitacaoResetRepository.Get(dto.Token);
                solicitacaoEntity.Desativar();
                await this._solicitacaoResetRepository.Update(solicitacaoEntity.Id, solicitacaoEntity);
            }
        }
        public async Task<SonhadorDto> Alterar(SonhadorDto dto)
        {
            var entity = (await this._sonhadorRepository.Get(dto.Id));

            await this._sonhadorRepository.Update(entity.Id, entity.AlterarDados(dto));

            return dto;
        }
        public async Task<string> Delete(string id)
        {
            await this._sonhadorRepository.Remove(id);

            return id;
        }
    }
}