using Borkfolio.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Borkfolio.Persistence
{
    public class BorkfolioDbContext : DbContext
    {
        public BorkfolioDbContext(DbContextOptions<BorkfolioDbContext> options)
            : base(options) { }

        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<BoardGame> BoardGames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BorkfolioDbContext).Assembly);

            modelBuilder.Entity<BoardGame>()
                .HasIndex(e => e.BoardGameGeekId)
                .IsUnique();
        }
    }
}
