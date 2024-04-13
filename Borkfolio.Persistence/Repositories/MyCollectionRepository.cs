using AutoMapper;
using Borkfolio.Application.Contracts.Infrastructure;
using Borkfolio.Application.Contracts.Persistence;
using Borkfolio.Application.Features.BoardGames.Queries.GetMyCollection;
using Borkfolio.Application.Models.BoardGameGeek;
using Microsoft.Extensions.Caching.Memory;

namespace Borkfolio.Persistence.Repositories
{
    public class MyCollectionRepository : IMyCollectionRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IBoardGameGeekService _boardGameGeekService;
        private readonly IMapper _mapper;
        private const string MyCollectionCacheKey = "MyCollectionCacheKey";


        public MyCollectionRepository(IMemoryCache memoryCache, IBoardGameGeekService boardGameGeekService, IMapper mapper)
        {
            _memoryCache = memoryCache;
            _boardGameGeekService = boardGameGeekService;
            _mapper = mapper;
        }

        public async Task<List<CollectionItemDto>> GetAllAsync()
        {
            if (
                !_memoryCache.TryGetValue(
                    MyCollectionCacheKey,
                    out List<CollectionItemDto>? myCollection
                )
            )
            {
                List<BggCollectionItem>? myCollectionFromBgg =
                    await _boardGameGeekService.GetMyCollection();

                myCollection = _mapper.Map<List<CollectionItemDto>>(myCollectionFromBgg);

                _memoryCache.Set(
                    MyCollectionCacheKey,
                    myCollection,
                    new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
                    }
                );
            }

            return myCollection ?? new List<CollectionItemDto>();
        }
    }
}
