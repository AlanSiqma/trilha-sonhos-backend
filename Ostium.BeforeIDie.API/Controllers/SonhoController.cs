using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Ostium.BeforeIDie.API.Model.Contracts.Respositories;
using Ostium.BeforeIDie.API.Model.Dto;
using Ostium.BeforeIDie.API.Model.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ostium.BeforeIDie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SonhoController : ControllerBase
    {
        private readonly ISonhoRepository _sonhoRepository;
        private readonly IMapper _mapper;
        public SonhoController(ISonhoRepository sonhadorRepository, IMapper mapper)
        {
            this._sonhoRepository = sonhadorRepository;
            this._mapper = mapper;

        }
        [HttpGet]
        public async Task<ActionResult<SonhosDto>> Get()
        {
            var entities = await this._sonhoRepository.Get();

            var map = this._mapper.Map<List<SonhoDto>>(entities);

            return Ok(new SonhosDto().AddSonhos(map));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SonhoDto>> Get(string Id)
        {
            var entity = await this._sonhoRepository.Get();

            var map = this._mapper.Map<SonhoDto>(entity);

            return Ok(map);
        }
       
        [HttpGet("sonhos-sonhador/{id}")]
        public async Task<ActionResult<List<SonhoDto>>> SonhoSonhador(string id)
        {
            var entities = await this._sonhoRepository.Get();

            entities = entities.Where(x => x.IdSonhador.Equals(id)).ToList();

            var map = this._mapper.Map<List<SonhoDto>>(entities);

            return Ok(map);
        }

        [HttpGet("sonhos-visibilidade/{id}")]
        public async Task<ActionResult<List<SonhoDto>>> SonhoVisibilidade( string Id )
        {
            var entities = await this._sonhoRepository.Get();

            entities = entities.Where(x => x.Visibilidade.Equals(Id)).ToList();

            var map = this._mapper.Map<List<SonhoDto>>(entities);

            return Ok(map);
        }

        [HttpGet("sonhos-status/{id}")]
        public async Task<ActionResult<List<SonhoDto>>> SonhoStatus( string Id )
        {
            var entities = await this._sonhoRepository.Get();

            entities = entities.Where(x => x.Status.Equals(Id.ToUpper())).ToList();

            var map = this._mapper.Map<List<SonhoDto>>(entities);

            return Ok(map);
        }

        
        [HttpPost("novo-sonho")]
        public async Task<ActionResult> Post(SonhoDto dto) {
        
            var map = this._mapper.Map<SonhoEntity>(dto);

            var result = new SonhosDto();

            var entit  = await this._sonhoRepository.Create(map);

            var sonhos = (await this._sonhoRepository.Get()).Select( x => new SonhoDto(x));

            result.AddSonhos(sonhos.ToList());

            result.SetSonhoDestaque(entit.Id);

            return Ok(result);
        }
        
        [HttpPut("alterar-sonho")]
        public async Task<ActionResult> Put(SonhoDto dto)
        {
            var map = this._mapper.Map<SonhoEntity>(dto);

            await this._sonhoRepository.Update(map.Id,map);

            return Ok(dto);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await this._sonhoRepository.Remove(id);

            return Ok( new {msg = "removido"});
        }
    }
}
