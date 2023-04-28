using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ROWalks.API.Data;
using ROWalks.API.Models.Domain;
using System.Reflection.Metadata.Ecma335;
using System.Xml.XPath;

namespace ROWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly ROWalksDbContext db;

        public SQLWalkRepository(ROWalksDbContext db)
        {
            this.db = db;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await db.Walks.AddAsync(walk);
            await db.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var toDeleteWalk = await db.Walks.FirstOrDefaultAsync(x => x.WalkdID == id);
            if(toDeleteWalk == null) { return null; }
            db.Walks.Remove(toDeleteWalk);
            await db.SaveChangesAsync();
            return toDeleteWalk;
        }


        public async Task<List<Walk>> GetAllAsync(string? filterOn = null,string? filterQuery = null,
            string? sortBy = null,bool? isAscending = true,
            int pageNumber = 1,int pageSize = 1000)
        {

            var walks = db.Walks.Include(x => x.Difficulty).Include(x => x.County).AsQueryable();

            //Filtering
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery)==false)
            {
                if(filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //Sorting
            if(string.IsNullOrWhiteSpace(sortBy)==false)
            {
                if(sortBy.Equals("Name",StringComparison.CurrentCultureIgnoreCase))
                {
                    walks = (bool)isAscending ? walks.OrderBy(x => x.Name): walks.OrderByDescending(x=>x.Name);

                }else if(sortBy.Equals("Length",StringComparison.CurrentCultureIgnoreCase))
                {
                    walks = (bool)isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            //Pagination
            var skipResults = (pageNumber - 1) * pageSize;
            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }


        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await db.Walks
            .Include(x => x.Difficulty)
            .Include(x => x.County)
            .FirstOrDefaultAsync(x => x.WalkdID == id);

        }

        public async Task<Walk?> UpdateAsync(Guid id,Walk walk)
        {
            var existingWalk = await db.Walks.FirstOrDefaultAsync(x => x.WalkdID == id);
            if(existingWalk == null) { return null; }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.Difficulty = walk.Difficulty;
            existingWalk.WalkImageURL = walk.WalkImageURL;
            existingWalk.CountyId = walk.CountyId;
            existingWalk.LengthInKm = walk.LengthInKm;

            await db.SaveChangesAsync();

            return existingWalk;

        }
    }
}
