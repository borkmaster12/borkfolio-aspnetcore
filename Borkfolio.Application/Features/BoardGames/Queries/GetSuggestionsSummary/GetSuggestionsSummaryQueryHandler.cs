using AutoMapper;
using Borkfolio.Application.Contracts.Persistence;
using MediatR;

namespace Borkfolio.Application.Features.BoardGames.Queries.GetSuggestionsSummary
{
    public class GetSuggestionsSummaryQueryHandler
        : IRequestHandler<GetSuggestionsSummaryQuery, List<GetSuggestionsSummaryDto>>
    {
        private readonly ISuggestionsRepository _suggestionsRepository;
        private IMapper _mapper;

        public GetSuggestionsSummaryQueryHandler(
            IMapper mapper,
            ISuggestionsRepository suggestionsRepository
        )
        {
            _mapper = mapper;
            _suggestionsRepository = suggestionsRepository;
        }

        public async Task<List<GetSuggestionsSummaryDto>> Handle(
            GetSuggestionsSummaryQuery request,
            CancellationToken cancellationToken
        )
        {
            return await _suggestionsRepository.GetSuggestionsSummary();
        }
    }
}
