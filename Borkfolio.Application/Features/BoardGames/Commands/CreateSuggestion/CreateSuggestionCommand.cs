using MediatR;

namespace Borkfolio.Application.Features.BoardGames.Commands.CreateSuggestion
{
    public class CreateSuggestionCommand : IRequest<CreateSuggestionCommandResponse>
    {
        public int BoardGameGeekId { get; set; }
    }
}
