using AutoMapper;
using Borkfolio.Application.Contracts.Infrastructure;
using MediatR;

namespace Borkfolio.Application.Features.BoardGames.Queries.SearchBoardGames
{
    public class SearchBoardGamesQueryHandler
        : IRequestHandler<SearchBoardGamesQuery, List<BoardGameSearchResultDto>>
    {
        private readonly IBoardGameGeekService _boardGameGeekService;
        private readonly IMapper _mapper;

        public SearchBoardGamesQueryHandler(
            IBoardGameGeekService boardGameGeekService,
            IMapper mapper
        )
        {
            _boardGameGeekService = boardGameGeekService;
            _mapper = mapper;
        }

        public async Task<List<BoardGameSearchResultDto>> Handle(
            SearchBoardGamesQuery request,
            CancellationToken cancellationToken
        )
        {
            var bggSearchResultSet = await _boardGameGeekService.SearchBoardGames(request.Name);

            return _mapper.Map<List<BoardGameSearchResultDto>>(bggSearchResultSet);
        }
    }
}
