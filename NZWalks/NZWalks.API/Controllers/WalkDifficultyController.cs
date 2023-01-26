using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.Interface;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
            var walkDifficulties = await walkDifficultyRepository.GetAll();
            var difficultyDT0 = mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDifficulties);
            return Ok(difficultyDT0);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficulty")]
        public async Task<IActionResult> GetWalkDifficulty(Guid id)
        {
            var difficulty = await walkDifficultyRepository.Get(id);
            if (difficulty == null)
            {
                return NotFound();
            }

            var difficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(difficulty);
            return Ok(difficultyDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficulty([FromBody] WalkDifficultyRequest request)
        {
            //if (!ValidateAddWalkDifficulty(request))
            //{
            //    return BadRequest(ModelState);
            //}

            //Convert DTO to Domain
            var difficulty = new Models.Domain.WalkDifficulty()
            {
                Code = request.Code
            };

            //Call Repository ADD method
            difficulty = await walkDifficultyRepository.Add(difficulty);

            //Convert Domain to DTO
            var difficultyDTO = new Models.DTO.WalkDifficulty()
            {
                Id = difficulty.Id,
                Code = difficulty.Code
            };

            //Return result
            return CreatedAtAction(nameof(GetWalkDifficulty), new { id = difficultyDTO.Id }, difficultyDTO);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficulty([FromRoute] Guid id, [FromBody] WalkDifficultyRequest request)
        {
            //if (!ValidateUpdateWalkDifficulty(request))
            //{
            //    return BadRequest(ModelState);
            //}

            var difficulty = new Models.Domain.WalkDifficulty()
            {
                Code = request.Code
            };

            difficulty = await walkDifficultyRepository.Update(id, difficulty);
            if (difficulty == null)
            {
                return NotFound();
            }

            var difficultyDTO = new Models.DTO.WalkDifficulty()
            {
                Id = difficulty.Id,
                Code = difficulty.Code
            };

            return Ok(difficultyDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficulty([FromRoute]Guid id)
        {
            var difficulty = await walkDifficultyRepository.Delete(id);
            if (difficulty == null)
            {
                return NotFound();
            }

            var difficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(difficulty);
            return Ok(difficultyDTO);
        }

        #region Private Methods

        private bool ValidateAddWalkDifficulty(WalkDifficultyRequest request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(request), $"Add walk difficulty data is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(request.Code))
            {
                ModelState.AddModelError(nameof(request.Code), $"{nameof(request.Code)} cannot be null or empty or whitespace.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateWalkDifficulty(WalkDifficultyRequest request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(request), $"Add walk difficulty data is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(request.Code))
            {
                ModelState.AddModelError(nameof(request.Code), $"{nameof(request.Code)} cannot be null or empty or whitespace.");
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
