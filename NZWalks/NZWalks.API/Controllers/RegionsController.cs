using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.Interface;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllRegions()
        {
            //var regions = regionRepository.GetAll();
            var regions = await regionRepository.GetAllAsync();

            //return Region DTO
            //var regionsDTO = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Code = region.Name,
            //        Name= region.Name,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population
            //    };

            //    regionsDTO.Add(regionDTO);
            //});

            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegion")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetRegion(Guid id)
        {
            var region = await regionRepository.Get(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        //[Authorize(Roles = "writer")]
        public async Task<IActionResult> AddRegion(AddRegionRequest request)
        {
            //Validation the request
            //if (!ValidateAddRegion(request))
            //{
            //    return BadRequest(ModelState);
            //}
            
            //Request to Domain Model
            var region = new Models.Domain.Region()
            {
                Code = request.Code,
                Area = request.Area,
                Lat = request.Lat,
                Long = request.Long,
                Name = request.Name,
                Population = request.Population
            };

            //Pass details to Repository
            region = await regionRepository.Add(region);

            //Convert back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long, 
                Name = region.Name,
                Population = region.Population
            };

            return CreatedAtAction(nameof(GetRegion), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            var region = await regionRepository.Delete(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateRegion([FromRoute]Guid id, [FromBody]UpdateRegionRequest region)
        {
            //Validating the region
            //if (!ValidateUpdateRegion(region))
            //{
            //    return BadRequest(ModelState);
            //}

            var existingRegion = new Models.Domain.Region()
            {
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            existingRegion = await regionRepository.Update(id, existingRegion);

            if (existingRegion == null)
            {
                return NotFound();
            }

            var regionDTO = new Models.DTO.Region()
            {
                Id = existingRegion.Id,
                Code = existingRegion.Code,
                Area = existingRegion.Area,
                Lat = existingRegion.Lat,
                Long = existingRegion.Long,
                Name = existingRegion.Name,
                Population = existingRegion.Population
            };

            return Ok(regionDTO);
        }


        #region Private Method

        private bool ValidateAddRegion(AddRegionRequest request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(request), $"Add Region Data is required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(request.Code))
            {
                ModelState.AddModelError(nameof(request.Code), $"{nameof(request.Code)} cannot be null or empty or blank.");
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                ModelState.AddModelError(nameof(request.Name), $"{nameof(request.Name)} cannot be null or empty or blank.");
            }

            if (request.Area <= 0)
            {
                ModelState.AddModelError(nameof(request.Area), $"{nameof(request.Area)} cannot be less than or equal to zero.");
            }

            if (request.Lat == 0)
            {
                ModelState.AddModelError(nameof(request.Lat), $"{nameof(request.Lat)} cannot be equal to zero.");
            }

            if (request.Long == 0)
            {
                ModelState.AddModelError(nameof(request.Long), $"{nameof(request.Long)} cannot be equal to zero.");
            }

            if (request.Population < 0)
            {
                ModelState.AddModelError(nameof(request.Population), $"{nameof(request.Population)} cannot be less than zero.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        private bool ValidateUpdateRegion(UpdateRegionRequest request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(UpdateRegionRequest), $"Update region data is required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                ModelState.AddModelError(nameof(request.Name), $"{nameof(request.Name)} cannot be null or empty or blank.");
            }

            if (request.Area <= 0)
            {
                ModelState.AddModelError(nameof(request.Area), $"{nameof(request.Area)} cannot be less than or equal to zero.");
            }

            if (request.Lat == 0)
            {
                ModelState.AddModelError(nameof(request.Lat), $"{nameof(request.Lat)} cannot be equal to zero.");
            }

            if (request.Long == 0)
            {
                ModelState.AddModelError(nameof(request.Long), $"{nameof(request.Long)} cannot be equal to zero.");
            }

            if (request.Population < 0)
            {
                ModelState.AddModelError(nameof(request.Population), $"{nameof(request.Population)} cannot be less than zero.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
