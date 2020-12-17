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
    public class VisibilidadeVisibilidadeSonhoController : ControllerBase
    {
        private readonly IVisibilidadeSonhoRepository _visibilidadeSonhoRepository;
        private readonly IMapper _mapper;
        public VisibilidadeVisibilidadeSonhoController(IVisibilidadeSonhoRepository sonhadorRepository, IMapper mapper)
        {
            this._visibilidadeSonhoRepository = sonhadorRepository;
            this._mapper = mapper;

        }
        [HttpGet]
        public async Task<ActionResult<List<VisibilidadeSonhoDto>>> Get()
        {
            var entities = await this._visibilidadeSonhoRepository.Get();

            var map = this._mapper.Map<List<VisibilidadeSonhoDto>>(entities);

            return Ok(map);
        }       

        [HttpPost("nova-visibilidade")]
        public async Task<ActionResult> Post(VisibilidadeSonhoDto dto) {
        
            var map = this._mapper.Map<VisibilidadeSonhoEntity>(dto);

            await this._visibilidadeSonhoRepository.Create(map);

            return Ok(dto);
        }
        
        [HttpPut("alterar-visibilidade")]
        public async Task<ActionResult> Put(VisibilidadeSonhoDto dto)
        {
            var map = this._mapper.Map<VisibilidadeSonhoEntity>(dto);

            await this._visibilidadeSonhoRepository.Update(map.Id,map);

            return Ok(dto);
        }
      
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await this._visibilidadeSonhoRepository.Remove(id);

            return Ok(id);
        }
    }
}
