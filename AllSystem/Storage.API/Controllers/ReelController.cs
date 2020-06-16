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
using Storage.API.Helpers;

namespace Storage.API.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class ReelController : ControllerBase
    {   
        
        private readonly IReelRepository _repo;
        private readonly IMapper _mapper;
        public ReelController(IReelRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetReels([FromQuery]ReelParams reelParams)
        {
            var reels = await _repo.GetReels(reelParams);
            var reelsToReturn= _mapper.Map<IEnumerable<ReelsForListDto>>(reels);

            Response.AddPagination(reels.CurrentPage, reels.PageSize, reels.TotalCount, reels.TotalPages);

            return Ok(reelsToReturn);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReel(int id)
        {
            var reel = await _repo.GetReel(id);
            var reelToReturn= _mapper.Map<ReelsForListDto>(reel);

            return Ok(reelToReturn);
        }
        [HttpPost("registerreel")]
        public async Task<IActionResult> RegisterReel(ReelForRegisterDto reelForRegisterDto)
        {
            var ReelToCreate = new Reel
            {
                CMnf = reelForRegisterDto.CMnf,
                QTY = reelForRegisterDto.QTY
            };

            var CreateReel = await _repo.RegisterReel(ReelToCreate);

            

            return StatusCode(201);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReel(int id, ReelForUpdateDto reelForUpdateDto)
        {
            // if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            //     return Unauthorized();

            var reelFromRepo = await _repo.GetReel(id);

            _mapper.Map(reelForUpdateDto, reelFromRepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating user {id} failed on save");
        }

    }
}