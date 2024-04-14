namespace Borkfolio.Application.Features.BoardGames.Queries.GetBoardGameDetails
{
    public class BoardGameDetailsDto
    {
        public int BoardGameGeekId { get; set; }
        public string Name { get; set; } = default!;
        public int Year { get; set; }
        public int MinimumAge { get; set; }
    }
}
