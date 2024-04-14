using Borkfolio.Domain.Common;

namespace Borkfolio.Domain.Entities
{
    public class Suggestion : AuditableEntity
    {
        public Guid SuggestionId { get; set; }
        public Guid BoardGameId { get; set; }
        public BoardGame BoardGame { get; set; } = default!;
    }
}
