namespace Borkfolio.Domain.Entities
{
    public class BoardGame
    {
        public int BoardGameId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Owned { get; set; }
        public int Year { get; set; }
    }
}
