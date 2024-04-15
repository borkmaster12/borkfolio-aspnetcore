using Borkfolio.Application.Contracts.Persistence;
using Borkfolio.Application.Features.BoardGames.Queries.GetSuggestionsSummary;
using Borkfolio.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Borkfolio.Persistence.Repositories
{
    public class SuggestionsRepository : BaseRepository<Suggestion>, ISuggestionsRepository
    {
        public SuggestionsRepository(BorkfolioDbContext dbContext)
            : base(dbContext) { }

        public Task<List<GetSuggestionsSummaryDto>> GetSuggestionsSummary()
        {
            return dbContext
                .Suggestions.Include(s => s.BoardGame)
                .GroupBy(
                    s => new
                    {
                        s.BoardGame.BoardGameGeekId,
                        s.BoardGame.Name,
                        s.BoardGame.Year
                    },
                    (group, suggestions) =>
                        new GetSuggestionsSummaryDto
                        {
                            BoardGameGeekId = group.BoardGameGeekId,
                            Name = group.Name,
                            Year = group.Year,
                            Count = suggestions.Count()
                        }
                )
                .ToListAsync();
        }

        public override Task<IReadOnlyList<Suggestion>> ListAllAsync()
        {
            return dbContext
                .Suggestions.Include(s => s.BoardGame)
                .ToListAsync()
                .ContinueWith(s => s.Result as IReadOnlyList<Suggestion>);
        }
    }
}
