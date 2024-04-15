using MediatR;

namespace Borkfolio.Application.Features.BoardGames.Queries.GetSuggestionsSummary
{
    public class GetSuggestionsSummaryQuery : IRequest<List<GetSuggestionsSummaryDto>>
    { }
}
