using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ostium.BeforeIDie.API.Model.Contracts.Respositories;
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
        private readonly IMapper _mapper;

        public SonhadorController(ISonhadorRepository sonhadorRepository, IMapper mapper)
        {
            this._sonhadorRepository = sonhadorRepository;
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
            
            if ( map != null ) 
                map.Senha = string.Empty;

            return Ok(map);
        }
        
        [HttpPost("entrar")]
        public async Task<ActionResult> Entrar(LoginDto dto)
        {
            SonhadorEntity usuario = (await this._sonhadorRepository.Get( x =>
                                                              x.Email.Equals(dto.Email) &&
                                                              x.Senha.Equals(dto.Password.Encrypt()))).FirstOrDefault();

            if(usuario != null) 
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

        [HttpPut("alterar-conta")]
        public async Task<ActionResult> Alterar(SonhadorDto dto)
        {
            var entity  = (await this._sonhadorRepository.Get(dto.Id));

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
