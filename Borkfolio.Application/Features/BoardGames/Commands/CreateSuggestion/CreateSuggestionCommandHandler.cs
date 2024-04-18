using AutoMapper;
using Borkfolio.Application.Contracts.Infrastructure;
using Borkfolio.Application.Contracts.Persistence;
using Borkfolio.Application.Models.BoardGameGeek;
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
        private readonly IBoardGameRepository _boardGameRepository;
        private readonly IProfanityCheckerService _profanityCheckerService;

        public CreateSuggestionCommandHandler(
            IAsyncRepository<Suggestion> suggestionsRepository,
            IBoardGameGeekService boardGameGeekService,
            IMapper mapper,
            IBoardGameRepository boardGameRepository,
            IProfanityCheckerService profanityCheckerService)
        {
            _suggestionsRepository = suggestionsRepository;
            _boardGameGeekService = boardGameGeekService;
            _mapper = mapper;
            _boardGameRepository = boardGameRepository;
            _profanityCheckerService = profanityCheckerService;
        }

        public async Task<CreateSuggestionCommandResponse> Handle(
            CreateSuggestionCommand request,
            CancellationToken cancellationToken
        )
        {
            var response = new CreateSuggestionCommandResponse();

            BggGameDetailsItem bggGameDetails;

            try
            {
                bggGameDetails = await _boardGameGeekService.GetBoardGameDetails(
                request.BoardGameGeekId
                );
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ValidationErrors = new List<string>() { ex.Message };
                return response;
            }

            var suggestionDto = _mapper.Map<CreateSuggestionDto>(bggGameDetails);

            var validator = new CreateSuggestionCommandValidator(_profanityCheckerService);

            var validationResult = await validator.ValidateAsync(suggestionDto);

            if (validationResult.Errors.Count > 0)
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                }
            }

            if (response.Success)
            {
                var boardGame = await _boardGameRepository.GetByBoardGameGeekIdAsync(
                    suggestionDto.BoardGameGeekId
                );

                boardGame ??= _mapper.Map<BoardGame>(suggestionDto);

                var suggestion = new Suggestion
                {
                    BoardGameId = boardGame.BoardGameId,
                    BoardGame = boardGame,
                    CreatedDate = DateTime.UtcNow,
                    LastModifiedDate = DateTime.UtcNow
                };

                await _suggestionsRepository.AddAsync(suggestion);

                response.Suggestion = suggestionDto;
            }

            return response;
        }
    }
}
