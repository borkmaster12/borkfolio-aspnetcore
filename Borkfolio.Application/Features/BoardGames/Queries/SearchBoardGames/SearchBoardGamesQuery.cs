using MediatR;

namespace Borkfolio.Application.Features.BoardGames.Queries.SearchBoardGames
{
    public class SearchBoardGamesQuery : IRequest<List<BoardGameSearchResultDto>>
    {
        public string Name { get; set; } = default!;
    }
}
