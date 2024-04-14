using Borkfolio.Application.Contracts.Persistence;
using Borkfolio.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Borkfolio.Persistence.Repositories
{
    public class BoardGameRepository : BaseRepository<BoardGame>, IBoardGameRepository
    {
        public BoardGameRepository(BorkfolioDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<BoardGame?> GetByBoardGameGeekIdAsync(int boardGameGeekId)
        {
            return await dbContext.BoardGames
                .Where(g => g.BoardGameGeekId == boardGameGeekId)
                .FirstOrDefaultAsync();
        }
    }
}
