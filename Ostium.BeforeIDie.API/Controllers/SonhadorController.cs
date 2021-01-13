using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ostium.BeforeIDie.API.Model.Contracts.Respositories;
using Ostium.BeforeIDie.API.Model.Dto;
using Ostium.BeforeIDie.API.Model.Entities;
using System.Collections.Generic;
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
            var entities = await this._sonhadorRepository.Get();
           
            var map = this._mapper.Map<List<SonhadorDto>>(entities);
           
            return Ok(new SonhadoresDto().AddSonhadores(map));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SonhadorDto>> Get(string id)
        {
            var entity = await this._sonhadorRepository.Get(id);
            var map = this._mapper.Map<SonhadorDto>(entity);
            
            if ( map != null ) map.Senha = string.Empty;

            return Ok(map);
        }
        
        [HttpPost("entrar")]
        public async Task<ActionResult> Entrar(LoginDto dto)
        {
            //TODO: implementar autenticacao
            SonhadorEntity usuario = await this._sonhadorRepository.Get( x =>
                                                              x.Email == dto.Email &&
                                                              x.Senha == dto.Password );

            if(usuario != null) usuario.Senha = string.Empty;

            return Ok(usuario);
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(SonhadorDto dto)
        {
            var map = this._mapper.Map<SonhadorEntity>(dto);

            await this._sonhadorRepository.Create(map);

            return Ok(dto);
        }

        [HttpPut("alterar-conta")]
        public async Task<ActionResult> Alterar(SonhadorDto dto)
        {
            var map = this._mapper.Map<SonhadorEntity>(dto);

            await this._sonhadorRepository.Update(map.Id,map);

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
