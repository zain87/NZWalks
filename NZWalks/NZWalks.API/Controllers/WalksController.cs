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
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;

        public WalksController(IWalkRepository walkRepository, IMapper mapper, IRegionRepository regionRepository, IWalkDifficultyRepository walkDifficultyRepository)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
            this.regionRepository = regionRepository;
            this.walkDifficultyRepository = walkDifficultyRepository;
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
        public async Task<IActionResult> AddWalk([FromBody]WalkRequest request)
        {
            if (!await ValidateAddWalk(request))
            {
                return BadRequest(ModelState);
            }

            //convert dto to domain
            var walk = new Models.Domain.Walk()
            {
                Name = request.Name,
                Length = request.Length,
                RegionId = request.RegionId,
                WalkDifficultyId = request.WalkDifficultyId
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

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalk([FromRoute]Guid id, [FromBody]WalkRequest request)
        {
            if (!await ValidateUpdateWalk(request))
            {
                return BadRequest(ModelState);
            }

            //convert dto to domain
            var walk = new Models.Domain.Walk()
            {
                Length = request.Length,
                Name = request.Name,
                RegionId = request.RegionId,
                WalkDifficultyId = request.WalkDifficultyId
            };

            //call update method from walk repo
            walk = await walkRepository.Update(id, walk);
            if (walk == null)
            {
                return NotFound();
            }

            //convert domain back to dto
            var walkDTO = new Models.DTO.Walk()
            {
                Length = walk.Length,
                Name = walk.Name,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId
            };


            //return result
            return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute]Guid id)
        {
            //delete from DB
            var walk = await walkRepository.Delete(id);
            if (walk == null)
            {
                return NotFound();
            }

            //convert domain to dto
            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);

            //diaplay result
            return Ok(walkDTO);
        }

        #region Private Methods

        private async Task<bool> ValidateAddWalk(WalkRequest request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(request), $"Add Walk data is required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                ModelState.AddModelError(nameof(request.Name), $"{nameof(request.Name)} cannot be null or empty or whitespace.");
            }

            if (request.Length <= 0)
            {
                ModelState.AddModelError(nameof(request.Length), $"{nameof(request.Length)} cannot be less than or equal to zero.");
            }

            var region = await regionRepository.Get(request.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(request.RegionId), $"{nameof(request.RegionId)} is invalid.");
            }

            var walkDifficulty = await walkDifficultyRepository.Get(request.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(request.WalkDifficultyId), $"{nameof(request.WalkDifficultyId)} is invalid.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> ValidateUpdateWalk(WalkRequest request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(request), $"Update Walk data is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                ModelState.AddModelError(nameof(request.Name), $"{nameof(request.Name)} cannot be null or empty or whitespace.");
            }

            if (request.Length <= 0)
            {
                ModelState.AddModelError(nameof(request.Length), $"{nameof(request.Length)} cannot be less than or equal to zero.");
            }

            var region = await regionRepository.Get(request.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(request.RegionId), $"{nameof(request.RegionId)} is invalid.");
            }

            var walkDifficulty = await walkDifficultyRepository.Get(request.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(request.WalkDifficultyId), $"{nameof(request.WalkDifficultyId)} is invalid.");
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
