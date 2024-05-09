using Borkfolio.Domain.Entities;

namespace Borkfolio.Persistence.IntegrationTests
{
    public static class Utilities
    {
        private static readonly int _seedCount = 10;

        public static async Task<int> SeedDatabase(BorkfolioDbContext context)
        {
            context.Database.EnsureCreated();

            for (int i = 1; i <= _seedCount; i++)
            {
                var boardGameId = Guid.NewGuid();
                context.Suggestions.Add(
                    new Suggestion
                    {
                        SuggestionId = Guid.NewGuid(),
                        BoardGameId = boardGameId,
                        BoardGame = new BoardGame
                        {
                            BoardGameId = boardGameId,
                            BoardGameGeekId = i,
                            Name = i.ToString(),
                            Year = 2000 + i
                        },
                        CreatedDate = DateTime.UtcNow,
                        LastModifiedDate = DateTime.UtcNow
                    }
                );
            }
            await context.SaveChangesAsync();

            return _seedCount;
        }
    }
}
