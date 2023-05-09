using AutoMapper;
using Ostium.BeforeIDie.Domain.Contracts.Repositories;
using Ostium.BeforeIDie.Domain.Contracts.Respositories;
using Ostium.BeforeIDie.Domain.Contracts.Services;
using Ostium.BeforeIDie.Domain.Dto;
using Ostium.BeforeIDie.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using Ostium.BeforeIDie.Domain.Extensions;

namespace Ostium.BeforeIDie.Services
{
    internal class SonhadorService
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
    }
}