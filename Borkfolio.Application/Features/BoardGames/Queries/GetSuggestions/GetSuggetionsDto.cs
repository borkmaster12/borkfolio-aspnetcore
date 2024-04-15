namespace Borkfolio.Application.Features.BoardGames.Queries.GetSuggestions
{
    public class GetSuggestionsDto
    {
        public int BoardGameGeekId { get; set; }
        public string Name { get; set; } = default!;
        public int Year { get; set; }
    }
}
