using AutoMapper;
using Borkfolio.Application.Contracts.Persistence;
using Borkfolio.Domain.Entities;
using MediatR;

namespace Borkfolio.Application.Features.BoardGames.Queries.GetSuggestions
{
    public class GetSuggestionsQueryHandler
        : IRequestHandler<GetSuggestionsQuery, List<GetSuggestionsDto>>
    {
        private readonly IAsyncRepository<Suggestion> _suggestionsRepository;
        private readonly IMapper _mapper;

        public GetSuggestionsQueryHandler(
            IAsyncRepository<Suggestion> suggestionsRepository,
            IMapper mapper
        )
        {
            _suggestionsRepository = suggestionsRepository;
            _mapper = mapper;
        }

        public async Task<List<GetSuggestionsDto>> Handle(
            GetSuggestionsQuery request,
            CancellationToken cancellationToken
        )
        {
            var suggestions = await _suggestionsRepository.ListAllAsync();
            var suggestionsDto = _mapper.Map<List<GetSuggestionsDto>>(suggestions);

            return suggestionsDto;
        }
    }
}
