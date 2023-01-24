using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.Interface;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var walks = await walkRepository.GetAll();
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walks);
            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalk")]
        public async Task<IActionResult> GetWalk(Guid id)
        {
            var walk = await walkRepository.Get(id);
            if (walk == null)
            {
                return NotFound();
            }

            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);
            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalk(WalkRequest request)
        {
            //convert dto to domain
            var walk = new Models.Domain.Walk()
            {
                Name = request.Name,
                Length = request.Length
            };

            //call repository Add method
            walk = await walkRepository.Add(walk);

            //Covert domain to dto
            var walkDTO = new Models.DTO.Walk()
            {
                Id = walk.Id,
                Name = walk.Name,
                Length = walk.Length,
                RegionId= walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId
            };

            //diaplay ok
            return CreatedAtAction(nameof(GetWalk), new { id = walkDTO.Id }, walkDTO);
        }


    }
}
