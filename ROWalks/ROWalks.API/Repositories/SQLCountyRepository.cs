using Microsoft.EntityFrameworkCore;
using ROWalks.API.Data;
using ROWalks.API.Models.Domain;

namespace ROWalks.API.Repositories
{
    public class SQLCountyRepository : ICountyRepository
    {
        private readonly ROWalksDbContext dbContext;

        public SQLCountyRepository(ROWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<County> CreateAsync(County county)
        {
            await dbContext.Counties.AddAsync(county);
            await dbContext.SaveChangesAsync();
            return county;
        }

        public async Task<County?> DeleteAsync(Guid id)
        {
            var existingCounty = await dbContext.Counties.FirstOrDefaultAsync(x => x.CountyId == id);
            if (existingCounty != null) { return null; }
            dbContext.Counties.Remove(existingCounty);
            await dbContext.SaveChangesAsync();
            return existingCounty;
        }

        public async Task<List<County>> GetAllAsync()
        {
            return await dbContext.Counties.ToListAsync();
        }

        public async Task<County?> GetAsync(Guid id)
        {
           return await dbContext.Counties.FirstOrDefaultAsync(x => x.CountyId == id);
        }

        public async Task<County?> UpdateAsync(Guid id,County county)
        {
            var existingCounty = await dbContext.Counties.FirstOrDefaultAsync(x => x.CountyId == id);

            if (existingCounty != null)
            {
                return null;
            }

            existingCounty.Code = county.Code;
            existingCounty.Name = county.Name;
            existingCounty.CountyImageURL = county.CountyImageURL;

            await dbContext.SaveChangesAsync();

            return existingCounty;

        }
    }
}
