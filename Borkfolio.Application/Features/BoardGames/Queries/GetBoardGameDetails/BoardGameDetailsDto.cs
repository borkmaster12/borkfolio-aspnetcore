namespace Borkfolio.Application.Features.BoardGames.Queries.GetBoardGameDetails
{
    public class BoardGameDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int Year { get; set; }
        public int MinimumAge { get; set; }
    }
}
