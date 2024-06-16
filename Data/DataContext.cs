using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Model;

namespace SuperHeroAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            // Additional configuration or initialization if needed
        }

        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}