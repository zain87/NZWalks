using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.Interface;

namespace NZWalks.API.Repositories.Implementation
{
    public class RegionRepository : IRegionRepository
    {
        public readonly ApplicationDbContext _db;
        public RegionRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public IEnumerable<Region> GetAll()
        {
            return _db.Regions.ToList();
        }
    }
}
