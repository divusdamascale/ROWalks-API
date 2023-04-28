using Microsoft.EntityFrameworkCore;
using SQLSeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SQLSeed.Data
{
    internal class SeedDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=ROWalksDb;Trusted_Connection=True;TrustServerCertificate=True");
        }

        public DbSet<County> Counties { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Walk> Walks { get; set; }

    }
}
