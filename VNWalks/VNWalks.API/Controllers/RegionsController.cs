using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VNWalks.API.CustomActionFilters;
using VNWalks.API.Data;
using VNWalks.API.Mappings;
using VNWalks.API.Models.Domain;
using VNWalks.API.Models.DTOs.Region;
using VNWalks.API.Repositories;



namespace VNWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class RegionsController : ControllerBase
    {
        private readonly VNWalksDbContext dbContext;
        private readonly IRegionRepositoty regionRepositoty;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor and Implement DbContext vao Controller
        /// </summary>
        /// <param name="dbContext">dbContext</param>
        /// 

        // Tiem Interface vao controller
        public RegionsController(VNWalksDbContext dbContext, IRegionRepositoty regionRepositoty, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepositoty = regionRepositoty; // khoi tao regionRepositoty
            //Su dung AutoMapper trong controller
            this.mapper = mapper;

        }


        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns>Region</returns>
        [HttpGet]
        [Authorize(Roles = "Reader")]
        // kieu tra ve cho cac phuong thuc khong dong bo la: Task<>
        public async Task<IActionResult> GetAll()
        {
            /* da co cac thuoc tinh ben trong DbContext class 
             * anh xa truc tiep toi bang Regions ben trong DB 
             * de chung ta co the nchung voi bang Regions */

            //Get data  from Database - Domain Models
            var regionsDomain = await regionRepositoty.GetAllAsync();

            //Map Domain Models -> DTOs
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

            //Return DTOs for client
            return Ok(regionsDto);
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <returns>ID of Region</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {

            //Get data  from Database - Domain Models
            var regionDomain = await regionRepositoty.GetByIdAsync(id);

            //Check if region don't exist
            if (regionDomain == null)
            {
                return NotFound();
            }

            //Map Region Domain Models -> DTOs
            var regionDto = mapper.Map<RegionDto>(regionDomain);

            //Return DTOs for client
            return Ok(regionDto);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <returns>new Region</returns>
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            // Map DTOs -> Domain Models
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            // Use Domain Models to Create Region by DbContext
            regionDomainModel = await regionRepositoty.CreateAsync(regionDomainModel);

            //Map Domain Models -> DTOs and Response for Client
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            //Create Object and Response Status 201
            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);

        }

        /// <summary>
        /// Update 
        /// </summary>
        /// <returns>Updated Region</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Map DTOs -> DomainModel
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            //Check if region exist?
            regionDomainModel = await regionRepositoty.UpdateAsync(id, regionDomainModel);

            //Check if regionDomainModel don't exist
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Check if regionDomainModel exist
            //Map Domain Model -> DTOs and response for client
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            //return 
            return Ok(regionDto);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Region</returns>
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //check region exist?
            var regionDomainModel = await regionRepositoty.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //C1: return empty Object
            //return OK(); 


            //C2: return Deleted Region Back
            //Map Domain Model -> DTOs
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }
    }
}
