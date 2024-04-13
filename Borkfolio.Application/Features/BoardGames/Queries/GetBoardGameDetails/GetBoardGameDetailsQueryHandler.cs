using AutoMapper;
using Borkfolio.Application.Contracts.Infrastructure;
using MediatR;

namespace Borkfolio.Application.Features.BoardGames.Queries.GetBoardGameDetails
{
    public class GetBoardGameDetailsQueryHandler
        : IRequestHandler<GetBoardGameDetailsQuery, BoardGameDetailsDto>
    {
        private readonly IBoardGameGeekService _boardGameGeekService;
        private readonly IMapper _mapper;

        public GetBoardGameDetailsQueryHandler(IBoardGameGeekService boardGameGeekService, IMapper mapper)
        {
            _boardGameGeekService = boardGameGeekService;
            _mapper = mapper;
        }

        public async Task<BoardGameDetailsDto> Handle(
            GetBoardGameDetailsQuery request,
            CancellationToken cancellationToken
        )
        {
            var bggGameDetails = await _boardGameGeekService.GetBoardGameDetails(request.Id);

            return _mapper.Map<BoardGameDetailsDto>(bggGameDetails);
        }
    }
}
