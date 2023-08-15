using Microsoft.EntityFrameworkCore;
using VNWalks.API.Models.Domain;

namespace VNWalks.API.Data
{
    /// <summary>
    /// Add a database context
    /// </summary>
    public class VNWalksDbContext : DbContext
    {

        public VNWalksDbContext(DbContextOptions<VNWalksDbContext> options) : base(options) { }

        public DbSet<Walk> Walks { get; set; }

        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Image> Images { get; set; }

    }
}
