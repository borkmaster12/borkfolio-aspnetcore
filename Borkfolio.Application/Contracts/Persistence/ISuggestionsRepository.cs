using Borkfolio.Application.Features.BoardGames.Queries.GetSuggestionsSummary;
using Borkfolio.Domain.Entities;

namespace Borkfolio.Application.Contracts.Persistence
{
    public interface ISuggestionsRepository : IAsyncRepository<Suggestion>
    {
        Task<List<GetSuggestionsSummaryDto>> GetSuggestionsSummary();
    }
}
