using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await _db.Regions.ToListAsync();
        }

        public async Task<Region> Get(Guid Id)
        {
            return await _db.Regions.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Region> Add(Region region)
        {
            region.Id = Guid.NewGuid();
            await _db.Regions.AddAsync(region);
            await _db.SaveChangesAsync();

            return region;
        }

        public async Task<Region> Delete(Guid Id)
        {
            var region = await _db.Regions.FirstOrDefaultAsync(x => x.Id == Id);

            if (region == null)
            {
                return null;
            }

            //Delete region from Database
            _db.Regions.Remove(region);
            await _db.SaveChangesAsync();

            return region;
        }

        public async Task<Region> Update(Guid Id, Region region)
        {
            var existingRegion = await _db.Regions.FirstOrDefaultAsync(x => x.Id == Id);

            if (existingRegion == null)
            {
                return null;
            }

            //existingRegion.Code = region.Code;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Name = region.Name;
            existingRegion.Population = region.Population;
            await _db.SaveChangesAsync();

            return existingRegion;
        }
    }
}
