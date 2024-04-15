namespace Borkfolio.Application.Features.BoardGames.Queries.GetSuggestionsSummary
{
    public class GetSuggestionsSummaryDto
    {
        public int BoardGameGeekId { get; set; }
        public string Name { get; set; } = default!;
        public int Year { get; set; }
        public int Count { get; set; }
    }
}
