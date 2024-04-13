using Borkfolio.Application.Contracts.Persistence;
using MediatR;

namespace Borkfolio.Application.Features.BoardGames.Queries.GetMyCollection
{
    public class GetMyCollectionQueryHandler
        : IRequestHandler<GetMyCollectionQuery, List<CollectionItemDto>>
    {
        private readonly IMyCollectionRepository _myCollectionRepository;

        public GetMyCollectionQueryHandler(IMyCollectionRepository myCollectionRepository)
        {
            _myCollectionRepository = myCollectionRepository;
        }

        public async Task<List<CollectionItemDto>> Handle(
            GetMyCollectionQuery request,
            CancellationToken cancellationToken
        )
        {
            return await _myCollectionRepository.GetAllAsync();
        }
    }
}
