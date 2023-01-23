using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories.Interface;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;

        public RegionsController(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        public IActionResult GetAllRegions()
        {
            var regions = regionRepository.GetAll();

            //return Region DTO
            var regionsDto = new List<Models.DTO.Region>();
            regions.ToList().ForEach(region =>
            {
                var regionDto = new Models.DTO.Region()
                {
                    Id = region.Id,
                    Code = region.Name,
                    Name= region.Name,
                    Area = region.Area,
                    Lat = region.Lat,
                    Long = region.Long,
                    Population = region.Population
                };

                regionsDto.Add(regionDto);
            });

            return Ok(regionsDto);
        }
    }
}
