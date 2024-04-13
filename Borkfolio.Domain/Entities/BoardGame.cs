namespace Borkfolio.Domain.Entities
{
    public class BoardGame
    {
        public int BoardGameId { get; set; }
        public int BoardGameGeekId { get; set; }
        public string Name { get; set; } = default!;
        public int Year { get; set; }
    }
}
