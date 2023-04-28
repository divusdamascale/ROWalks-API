using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ROWalks.API.CustomActionFilters;
using ROWalks.API.Data;
using ROWalks.API.Models.Domain;
using ROWalks.API.Models.DTO;
using ROWalks.API.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace ROWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        public readonly IMapper mapper;
        private readonly IWalkRepository sqlWalkRepository;

        public WalksController(IMapper mapper,IWalkRepository sqlWalkRepository)
        {
            this.mapper = mapper;
            this.sqlWalkRepository = sqlWalkRepository;
        }
        //CreateWalk
        [HttpPost("CreateWalk")]
        [ValidateModel]

        public async Task<IActionResult> Create([FromBody] CreateWalkDto createWalkDto)
        {

                //Map to 
                var walkDomain = mapper.Map<Walk>(createWalkDto);

                await sqlWalkRepository.CreateAsync(walkDomain);
                return Ok();
          
        }

        //GetWalks
        [HttpGet("GetWalks")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn,[FromQuery]string? filterQuery,
            [FromQuery] string? sortBy,[FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1,[FromQuery]int pageSize=1000)
        {
            var walksDomain = await sqlWalkRepository.GetAllAsync(filterOn,filterQuery,
                sortBy,isAscending,
                pageNumber,pageSize);

            return Ok(mapper.Map<List<WalkDto>>(walksDomain));
        }

        [HttpGet]
        [Route("GetById/{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walk = await sqlWalkRepository.GetByIdAsync(id);
            if(walk == null) return NotFound();
            return Ok(mapper.Map<WalkDto>(walk));
        }

        [HttpPut]
        [Route("UpdateWalk/{id:Guid}")]
        [ValidateModel]

        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id,UpdateWalkDto updateWalkDto)
        {

           
                var domain = mapper.Map<Walk>(updateWalkDto);
                domain = await sqlWalkRepository.UpdateAsync(id,domain);
                if(domain == null)
                {
                    return NotFound();
                }

                return Ok(mapper.Map<WalkDto>(domain));
            

            
        }


        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalk = await sqlWalkRepository.DeleteAsync(id);
            if(deletedWalk == null) return NotFound();

            return Ok(mapper.Map<WalkDto>(deletedWalk));
    }
    }



}
