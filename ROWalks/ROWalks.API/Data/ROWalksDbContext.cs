using Microsoft.EntityFrameworkCore;
using ROWalks.API.Models.Domain;

namespace ROWalks.API.Data
{
    public class ROWalksDbContext: DbContext
    {
        public ROWalksDbContext(DbContextOptions<ROWalksDbContext> dbContextOptions) :base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
