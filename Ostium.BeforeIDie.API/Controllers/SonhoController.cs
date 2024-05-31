using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ostium.BeforeIDie.Domain.Contracts.Respositories;
using Ostium.BeforeIDie.Domain.Dto;
using Ostium.BeforeIDie.Domain.Entities;
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
        public SonhoController(ISonhoRepository sonhadorRepository)
        {
            this._sonhoRepository = sonhadorRepository;
        }
        [HttpGet]
        public async Task<ActionResult<SonhosDto>> Get()
        {
            var entities = await this._sonhoRepository.Get();

            var map = entities.Select(x => new SonhoDto(x)).ToList();

            return Ok(new SonhosDto().AddSonhos(map));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SonhoDto>> Get(string id)
        {
            var entity = await this._sonhoRepository.Get(id);

            var map = new SonhoDto(entity);

            return Ok(map);
        }
       
        [HttpGet("sonhos-sonhador/{id}")]
        public async Task<ActionResult<List<SonhoDto>>> SonhoSonhador(string id)
        {
            var entities = await this._sonhoRepository.Get();

            entities = entities.Where(x => x.IdSonhador.Equals(id)).ToList();

            var map = entities.Select(x => new SonhoDto(x)).ToList();

            return Ok(new SonhosDto().AddSonhos(map));
        }

        [HttpGet("sonhos-visibilidade/{id}")]
        public async Task<ActionResult<List<SonhoDto>>> SonhoVisibilidade( string Id )
        {
            var entities = await this._sonhoRepository.Get();

            entities = entities.Where(x => x.Visibilidade.Equals(Id)).ToList();

            var map = entities.Select(x => new SonhoDto(x)).ToList();

            return Ok(new SonhosDto().AddSonhos(map));
        }

        [HttpGet("sonhos-status/{id}")]
        public async Task<ActionResult<List<SonhoDto>>> SonhoStatus( string Id )
        {
            var entities = await this._sonhoRepository.Get();

            entities = entities.Where(x => x.Status.Equals(Id.ToUpper())).ToList();

            var map = entities.Select(x => new SonhoDto(x)).ToList();

            return Ok(new SonhosDto().AddSonhos(map));
        }

        
        [HttpPost("novo-sonho")]
        public async Task<ActionResult> Post(SonhoDto dto) {
        
            var entity = new SonhoEntity(dto);

            var result = new SonhosDto();

            var entit  = await this._sonhoRepository.Create(entity);

            var sonhos = (await this._sonhoRepository.Get()).Select( x => new SonhoDto(x));

            result.AddSonhos(sonhos.ToList());

            result.SetSonhoDestaque(entit.Id);

            return Ok(result);
        }
        
        [HttpPut("alterar-sonho")]
        public async Task<ActionResult> Put(SonhoDto dto)
        {
            var map = new SonhoEntity(dto);

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
