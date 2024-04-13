using MediatR;

namespace Borkfolio.Application.Features.BoardGames.Queries.GetMyCollection
{
    public class GetMyCollectionQuery : IRequest<List<CollectionItemDto>>
    {
    }
}
