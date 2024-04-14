using AutoMapper;
using Borkfolio.Application.Features.BoardGames.Commands.CreateSuggestion;
using Borkfolio.Application.Features.BoardGames.Queries.GetBoardGameDetails;
using Borkfolio.Application.Features.BoardGames.Queries.GetMyCollection;
using Borkfolio.Application.Features.BoardGames.Queries.SearchBoardGames;
using Borkfolio.Application.Models.BoardGameGeek;
using Borkfolio.Domain.Entities;

namespace Borkfolio.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BggCollectionItem, CollectionItemDto>()
                .ForMember(dest => dest.BoardGameGeekId, act => act.MapFrom(src => src.Id)); ;

            CreateMap<BggGameDetailsItem, BoardGameDetailsDto>()
                .ForMember(dest => dest.BoardGameGeekId, act => act.MapFrom(src => src.Id));

            CreateMap<BggSearchResultItem, BoardGameSearchResultDto>()
                .ForMember(dest => dest.BoardGameGeekId, act => act.MapFrom(src => src.Id));

            CreateMap<BggGameDetailsItem, BoardGame>()
                .ForMember(dest => dest.BoardGameGeekId, act => act.MapFrom(src => src.Id));

            CreateMap<BggGameDetailsItem, CreateSuggestionDto>()
                .ForMember(dest => dest.BoardGameGeekId, act => act.MapFrom(src => src.Id));

            CreateMap<CreateSuggestionDto, BoardGame>();
        }
    }
}
