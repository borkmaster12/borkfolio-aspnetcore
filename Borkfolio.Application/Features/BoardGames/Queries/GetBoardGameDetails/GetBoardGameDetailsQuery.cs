using MediatR;

namespace Borkfolio.Application.Features.BoardGames.Queries.GetBoardGameDetails
{
    public class GetBoardGameDetailsQuery : IRequest<BoardGameDetailsDto>
    {
        public int Id { get; set; }
    }
}
