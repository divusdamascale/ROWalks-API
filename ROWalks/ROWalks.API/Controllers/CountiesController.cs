using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ROWalks.API.CustomActionFilters;
using ROWalks.API.Data;
using ROWalks.API.Mappings;
using ROWalks.API.Models.Domain;
using ROWalks.API.Models.DTO;
using ROWalks.API.Repositories;
using System.Text.Json;

namespace ROWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountiesController : ControllerBase
    {
        public readonly ICountyRepository countyRepository;
        private readonly IMapper mapper;
        private readonly ILogger<CountiesController> logger;

        public CountiesController(ICountyRepository countyRepository,IMapper mapper,
            ILogger<CountiesController> logger)
        {
            this.countyRepository = countyRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        
        [HttpGet("GetAll")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation("GetAll Counties Action Method was invoked");
            //Get Data From Database -Domain Models
            var counties = await countyRepository.GetAllAsync();

          //map to dto
            var countiesDto =  mapper.Map<List<CountyDto>>(counties);

            //Return Dtos

            logger.LogInformation($"Finished GetAllRegions request with data : {JsonSerializer.Serialize(countiesDto)}");
            return Ok(countiesDto);
        }

        //Get single by id
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var county = await countyRepository.GetAsync(id);

           
            if(county == null) 
            {
                return NotFound();
            }

            //Map to 
            return Ok(mapper.Map<CountyDto>(county));
        }

        //POST Create new County
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddCountyRequestDto addCountyRequestDto)
        {

            
                //Map DTO to Domain Model
                var countyDomainModel = mapper.Map<County>(addCountyRequestDto);

                //Use Domain Model to create Region
                countyDomainModel = await countyRepository.CreateAsync(countyDomainModel);

                //Map Domain back

                var countyDto = mapper.Map<CountyDto>(countyDomainModel);
                return CreatedAtAction(nameof(GetById),new { id = countyDomainModel.CountyId },countyDto);
          
        }

        //Update County
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateCounty([FromRoute] Guid id,[FromBody] UpdateCountyDto updateCountyDto)
        {
           
                //Map DTO to domain Model

                var countyDomainModel = mapper.Map<County>(updateCountyDto);

                await countyRepository.UpdateAsync(id,countyDomainModel);

                if(countyDomainModel == null) return NotFound();

                //domain Model to dto

                var countyDto = mapper.Map<CountyDto>(countyDomainModel);

                return Ok(countyDto);

         
            
        }

        //Delete County
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> DeleteCounty([FromRoute] Guid id) 
        {
            var countyToDelete = await countyRepository.DeleteAsync(id);

            if( countyToDelete == null) return NotFound();

            var countyDeletedDto = mapper.Map<CountyDto>(countyToDelete);

            return Ok(countyDeletedDto);


        }
    }
}
