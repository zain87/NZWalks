using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Interface
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAll();
        Task<WalkDifficulty> Get(Guid Id);
        Task<WalkDifficulty> Add(WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> Update(Guid Id, WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> Delete(Guid Id);
    }
}
