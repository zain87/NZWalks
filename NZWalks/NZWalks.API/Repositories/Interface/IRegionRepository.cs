using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Interface
{
    public interface IRegionRepository
    {
        IEnumerable<Region> GetAll();

        Task<IEnumerable<Region>> GetAllAsync();

        Task<Region> Get(Guid Id);

        Task<Region> Add(Region region);

        Task<Region> Delete(Guid Id);

        Task<Region> Update(Guid Id, Region region);
    }
}
