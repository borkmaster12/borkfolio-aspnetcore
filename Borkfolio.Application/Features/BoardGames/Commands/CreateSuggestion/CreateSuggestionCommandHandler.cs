using AutoMapper;
using Borkfolio.Application.Contracts.Infrastructure;
using Borkfolio.Application.Contracts.Persistence;
using Borkfolio.Domain.Entities;
using MediatR;

namespace Borkfolio.Application.Features.BoardGames.Commands.CreateSuggestion
{
    public class CreateSuggestionCommandHandler
        : IRequestHandler<CreateSuggestionCommand, CreateSuggestionCommandResponse>
    {
        private readonly IAsyncRepository<Suggestion> _suggestionsRepository;
        private readonly IBoardGameGeekService _boardGameGeekService;
        private readonly IMapper _mapper;

        public CreateSuggestionCommandHandler(
            IAsyncRepository<Suggestion> suggestionsRepository,
            IBoardGameGeekService boardGameGeekService,
            IMapper mapper
        )
        {
            _suggestionsRepository = suggestionsRepository;
            _boardGameGeekService = boardGameGeekService;
            _mapper = mapper;
        }

        public async Task<CreateSuggestionCommandResponse> Handle(
            CreateSuggestionCommand request,
            CancellationToken cancellationToken
        )
        {
            var response = new CreateSuggestionCommandResponse();

            var bggDetails = await _boardGameGeekService.GetBoardGameDetails(
                request.BoardGameGeekId
            );

            var suggestion = _mapper.Map<CreateSuggestionDto>(bggDetails);

            var validator = new CreateSuggestionCommandValidator();

            var validationResult = await validator.ValidateAsync(suggestion);

            if (validationResult.Errors.Count > 0)
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                }
            }

            if (response.Success) { }

            return response;
        }
    }
}
