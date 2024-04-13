using Borkfolio.Application.Models.BoardGameGeek;

namespace Borkfolio.Application.Contracts.Infrastructure
{
    public interface IBoardGameGeekService
    {
        Task<List<BggCollectionItem>> GetMyCollection();
        Task<List<BggSearchResultItem>> SearchBoardGames(string name);
        Task<BggGameDetailsItem> GetBoardGameDetails(int id);
    }
}
