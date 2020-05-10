using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Storage.API.Data;
using Storage.API.DTOs;
using Storage.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Storage.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchRepository _repo;
        private readonly IMapper _mapper;
        public SearchController(ISearchRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpGet]
        public async Task<IActionResult> GetComponents()
        {
            var components = await _repo.GetComponents();
            var componentsToReturn= _mapper.Map<IEnumerable<ComponetsForListDto>>(components);

            return Ok(componentsToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComponents(int id)
        {   
            var components = await _repo.GetComponents(id);
            var componentsToReturn= _mapper.Map<ComponetsForListDto>(components);

            return Ok(componentsToReturn);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComponent(int id, ComponentForUpdateDto componentForUpdateDto)
        {
           // if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
           //     return Unauthorized();

            var componentsFromRepo = await _repo.GetComponents(id);

            _mapper.Map(componentForUpdateDto, componentsFromRepo);

            if (await _repo.SaveAll())
                return NoContent();
            
            throw new Exception($"Updating user {id} failed on save");
        }
         [HttpPost("registercomponent")]
        public async Task<IActionResult> RegisterComponent(ComponetsForRegisterDto ComponetsForRegisterDto)
        {

            var ComponentasToCreate = new Componentas
            {   
                Mnf = ComponetsForRegisterDto.Mnf,
                Manufacturer = ComponetsForRegisterDto.Manufacturer,
                Detdescription = ComponetsForRegisterDto.Detdescription,
                BuhNr = ComponetsForRegisterDto.BuhNr,
                Size = ComponetsForRegisterDto.Size,
                Type = ComponetsForRegisterDto.Type,
                Nominal = ComponetsForRegisterDto.Nominal,
                Furl = ComponetsForRegisterDto.Furl,
                Durl = ComponetsForRegisterDto.Durl,
                Murl = ComponetsForRegisterDto.Murl
            };

            var createComponent = await _repo.RegisterComponents(ComponentasToCreate);
            
            return StatusCode(201);
        }
        
        


    }
}