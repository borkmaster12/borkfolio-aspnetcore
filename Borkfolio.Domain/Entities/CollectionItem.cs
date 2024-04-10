namespace Borkfolio.Domain.Entities
{
    public class CollectionItem
    {
        public int CollectionItemId { get; set; }
        public int BoardGameId { get; set; }
        public BoardGame BoardGame { get; set; } = default!;

    }
}
