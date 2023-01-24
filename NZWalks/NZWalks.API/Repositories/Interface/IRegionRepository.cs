using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Interface
{
    public interface IRegionRepository
    {
        IEnumerable<Region> GetAll();

        Task<IEnumerable<Region>> GetAllAsync();
    }
}
