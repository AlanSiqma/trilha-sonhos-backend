using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Ostium.BeforeIDie.API.Model.Contracts.Respositories;
using Ostium.BeforeIDie.API.Model.Dto;
using Ostium.BeforeIDie.API.Model.Entities;
using System;
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
            return Ok(new SonhadorDto());
        }
        [HttpPost("entrar")]
        public async Task<ActionResult> Entrar(LoginDto dto)
        {

            return Ok(dto);
        }
        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(RegisterUserDto registerUser)
        {
          
            return Ok(registerUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {

            return Ok(id);
        }

    }
}
