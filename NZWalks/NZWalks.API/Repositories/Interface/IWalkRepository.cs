using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Interface
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAll();
        Task<Walk> Get(Guid Id);
        Task<Walk> Add(Walk walk);
        Task<Walk> Update(Guid Id, Walk walk);
        Task<Walk> Delete(Guid Id);
    }
}
