namespace Borkfolio.Application.Features.BoardGames.Queries.GetMyCollection
{
    public class CollectionItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int Year { get; set; }
    }
}
