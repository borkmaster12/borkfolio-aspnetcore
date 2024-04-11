using Borkfolio.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Borkfolio.Persistence
{
    public class BorkfolioDbContext : DbContext
    {
        public BorkfolioDbContext(DbContextOptions<BorkfolioDbContext> options)
            : base(options) { }

        public DbSet<BoardGame> BoardGames { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }

    }
}
