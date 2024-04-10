using Borkfolio.Domain.Common;

namespace Borkfolio.Domain.Entities
{
    public class Suggestion : AuditableEntity
    {
        public int SuggestionId { get; set; }
        public int BoardGameId { get; set; }
        public BoardGame BoardGame { get; set; } = default!;
    }
}
