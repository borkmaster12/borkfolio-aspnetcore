using Borkfolio.Application.Features.BoardGames.Queries.GetMyCollection;

namespace Borkfolio.Application.Contracts.Persistence
{
    public interface IMyCollectionRepository
    {
        public Task<List<CollectionItemDto>> GetAllAsync();
    }
}
