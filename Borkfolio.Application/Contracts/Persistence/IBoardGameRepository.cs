using Borkfolio.Domain.Entities;

namespace Borkfolio.Application.Contracts.Persistence
{
    public interface IBoardGameRepository : IAsyncRepository<BoardGame>
    {
        Task<BoardGame?> GetByBoardGameGeekIdAsync(int boardGameGeekId);
    }
}
