using MediatR;

namespace Borkfolio.Application.Features.BoardGames.Queries.GetSuggestions
{
    public class GetSuggestionsQuery : IRequest<List<GetSuggestionsDto>>
    { }
}
