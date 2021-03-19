using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ostium.BeforeIDie.API.Model.Contracts.Repositories;
using Ostium.BeforeIDie.API.Model.Dto;
using Ostium.BeforeIDie.API.Model.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ostium.BeforeIDie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeloTrilhaController : ControllerBase
    {
        private readonly IModeloTrilhaRepository _modeloTrilhaRepository;
        private readonly IMapper _mapper;
        public ModeloTrilhaController(IModeloTrilhaRepository modeloTrilhaRepository, IMapper mapper)
        {
            this._modeloTrilhaRepository = modeloTrilhaRepository;
            this._mapper = mapper;

        }
        [HttpGet]
        public async Task<ActionResult<List<ModeloTrilhaDto>>> Get()
        {
            var entities = await this._modeloTrilhaRepository.Get();

            var map = this._mapper.Map<List<ModeloTrilhaDto>>(entities);

            return Ok(map);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ModeloTrilhaDto>> Get(string id)
        {
            var entity = await this._modeloTrilhaRepository.Get(id);

            var map = this._mapper.Map<ModeloTrilhaDto>(entity);

            return Ok(map);
        }

        [HttpGet("trilhas-modelos-usuario/{id}")]
        public async Task<ActionResult<List<ModeloTrilhaDto>>> TrilhasModeloUsuario(string id)
        {
            var entities = await this._modeloTrilhaRepository.Get();

            entities = entities.Where(x => x.IdUsuarioRegistro.Equals(id)).ToList();

            var map = this._mapper.Map<List<ModeloTrilhaDto>>(entities);

            return Ok(map);
        }

        [HttpPost("nova-trilha")]
        public async Task<ActionResult> Post(ModeloTrilhaDto dto)
        {

            var map = this._mapper.Map<ModeloTrilhaEntity>(dto);

            await this._modeloTrilhaRepository.Create(map);

            return Ok(dto);
        }

        [HttpPut("alterar-modelo-trilha")]
        public async Task<ActionResult> Put(ModeloTrilhaDto dto)
        {
            var map = this._mapper.Map<ModeloTrilhaEntity>(dto);

            await this._modeloTrilhaRepository.Update(map.Id, map);

            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await this._modeloTrilhaRepository.Remove(id);

            return Ok(new { msg = "removido" });
        }
    }
}
