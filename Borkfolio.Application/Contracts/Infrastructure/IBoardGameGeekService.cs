using Borkfolio.Application.Models.BoardGameGeek;

namespace Borkfolio.Application.Contracts.Infrastructure
{
    public interface IBoardGameGeekService
    {
        Task<BggCollection> GetMyCollection();
        Task<BggSearchResultSet> SearchBoardGames(string name);
        Task<BggBoardGameDetails> GetBoardGameDetails(int it);
    }
}
