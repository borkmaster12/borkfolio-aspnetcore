using Borkfolio.Application.Responses;

namespace Borkfolio.Application.Features.BoardGames.Commands.CreateSuggestion
{
    public class CreateSuggestionCommandResponse : BaseResponse
    {
        public CreateSuggestionCommandResponse() : base() { }

        public CreateSuggestionDto Suggestion { get; set; } = default!;
    }
}
