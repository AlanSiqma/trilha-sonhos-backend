using AutoMapper;
using Ostium.BeforeIDie.Domain.Contracts.Repositories;
using Ostium.BeforeIDie.Domain.Contracts.Respositories;
using Ostium.BeforeIDie.Domain.Contracts.Services;
using Ostium.BeforeIDie.Domain.Dto;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<SonhadoresDto> GetSonhadores()
        {
            var map = await this._sonhadorRepository.Get();

            var listSonhadorDto = map.Select(x => new SonhadorDto(x)).ToList();

            return new SonhadoresDto().AddSonhadores(listSonhadorDto);
        }
    }
}