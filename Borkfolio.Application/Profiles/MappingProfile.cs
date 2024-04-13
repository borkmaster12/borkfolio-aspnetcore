using AutoMapper;
using Borkfolio.Application.Features.BoardGames.Commands.CreateSuggestion;
using Borkfolio.Application.Features.BoardGames.Queries.GetBoardGameDetails;
using Borkfolio.Application.Features.BoardGames.Queries.GetMyCollection;
using Borkfolio.Application.Features.BoardGames.Queries.SearchBoardGames;
using Borkfolio.Application.Models.BoardGameGeek;

namespace Borkfolio.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BggCollectionItem, CollectionItemDto>();
            CreateMap<BggGameDetailsItem, BoardGameDetailsDto>();
            CreateMap<BggSearchResultItem, BoardGameSearchResultDto>();
            CreateMap<BggGameDetailsItem, CreateSuggestionDto>();
        }
    }
}
