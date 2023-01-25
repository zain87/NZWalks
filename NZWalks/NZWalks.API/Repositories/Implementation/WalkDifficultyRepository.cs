using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.Interface;

namespace NZWalks.API.Repositories.Implementation
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly ApplicationDbContext _db;

        public WalkDifficultyRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public async Task<WalkDifficulty> Add(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await _db.WalkDifficulty.AddAsync(walkDifficulty);
            await _db.SaveChangesAsync();

            return walkDifficulty;
        }

        public async Task<WalkDifficulty> Delete(Guid Id)
        {
            var existingWalkDifficulty = await _db.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == Id);
            if (existingWalkDifficulty == null)
            {
                return null;
            }

            _db.WalkDifficulty.Remove(existingWalkDifficulty);
            await _db.SaveChangesAsync();

            return existingWalkDifficulty; 
        }

        public async Task<WalkDifficulty> Get(Guid Id)
        {
           return await _db.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAll()
        {
            return await _db.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> Update(Guid Id, WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await _db.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == Id);
            if (existingWalkDifficulty == null)
            {
                return null;
            }

            existingWalkDifficulty.Code = walkDifficulty.Code;
            await _db.SaveChangesAsync();

            return existingWalkDifficulty;
        }
    }
}
