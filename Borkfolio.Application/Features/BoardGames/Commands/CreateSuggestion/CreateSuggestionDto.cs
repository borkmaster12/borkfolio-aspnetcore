namespace Borkfolio.Application.Features.BoardGames.Commands.CreateSuggestion
{
    public class CreateSuggestionDto
    {
        public string Name { get; set; } = default!;
        public int MinimumAge { get; set; }
    }
}
