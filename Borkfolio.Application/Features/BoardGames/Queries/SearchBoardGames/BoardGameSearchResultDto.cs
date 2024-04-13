namespace Borkfolio.Application.Features.BoardGames.Queries.SearchBoardGames
{
    public class BoardGameSearchResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int Year { get; set; }
    }
}
