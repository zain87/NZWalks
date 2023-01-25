using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.Interface;

namespace NZWalks.API.Repositories.Implementation
{
    public class WalkRepository : IWalkRepository
    {
        private readonly ApplicationDbContext _db;

        public WalkRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public async Task<Walk> Add(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await _db.Walks.AddAsync(walk);
            await _db.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk> Delete(Guid Id)
        {
            var walk = await _db.Walks.FirstOrDefaultAsync(x => x.Id == Id);

            if (walk == null)
            { 
                return null;
            }

            _db.Walks.Remove(walk);
            await _db.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk> Get(Guid Id)
        {
            return await _db.Walks
                .Include(x => x.Region)
                .Include(X => X.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == Id);

        }

        public async Task<IEnumerable<Walk>> GetAll()
        {
            return await _db.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> Update(Guid Id, Walk walk)
        {
            var existingWalk = await _db.Walks.FirstOrDefaultAsync(x => x.Id == Id);
            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Length = walk.Length;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
            await _db.SaveChangesAsync();

            return existingWalk;
        }
    }
}
