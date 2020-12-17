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
    public class StatusSonhoController : ControllerBase
    {
        private readonly IStatusSonhoRepository _statusRepository;
        private readonly IMapper _mapper;
        public StatusSonhoController(IStatusSonhoRepository statusRepository, IMapper mapper)
        {
            this._statusRepository = statusRepository;
            this._mapper = mapper;

        }
        
        [HttpGet]
        public async Task<ActionResult<List<StatusSonhosDto>>> Get()        {
            var entities = await this._statusRepository.Get();

            var map = this._mapper.Map<List<StatusSonhosDto>>(entities);

            return Ok(map);
        }

        [HttpPost("novo-status")]
        public async Task<ActionResult> Post(StatusSonhosDto dto) {
        
            var map = this._mapper.Map<StatusSonhoEntity>(dto);

            await this._statusRepository.Create(map);

            return Ok(dto);
        }
       
        [HttpPut("alterar-status")]
        public async Task<ActionResult> Put(StatusSonhosDto dto)
        {
            var map = this._mapper.Map<StatusSonhoEntity>(dto);

            await this._statusRepository.Update(map.Id,map);

            return Ok(dto);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await this._statusRepository.Remove(id);

            return Ok(id);
        }
    }
}
