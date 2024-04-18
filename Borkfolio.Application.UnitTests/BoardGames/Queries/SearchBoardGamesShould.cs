using AutoMapper;
using Borkfolio.Application.Contracts.Infrastructure;
using Borkfolio.Application.Features.BoardGames.Queries.SearchBoardGames;
using Borkfolio.Application.Models.BoardGameGeek;
using Borkfolio.Application.Profiles;
using Moq;

namespace Borkfolio.Application.UnitTests.BoardGames.Queries
{
    public class SearchBoardGamesShould
    {
        private readonly IMapper _mapper;

        private Mock<IBoardGameGeekService> _mockBoardGameGeekService;

        public SearchBoardGamesShould()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [SetUp]
        public void Setup()
        {
            _mockBoardGameGeekService = new Mock<IBoardGameGeekService>();
        }

        [Test]
        public async Task GetSearchResultsFromBoardGameGeekService()
        {
            string query = "wingspan";
            _mockBoardGameGeekService
                .Setup(s => s.SearchBoardGames(query))
                .ReturnsAsync(
                    new List<BggSearchResultItem>
                    {
                        new BggSearchResultItem
                        {
                            Id = 266192,
                            _name = new BggSearchResultItem.NameElement { Value = "Wingspan" },
                            _year = new BggSearchResultItem.YearElement { Value = "2019" }
                        }
                    }
                );

            var request = new SearchBoardGamesQuery { Name = query };

            var sut = new SearchBoardGamesQueryHandler(_mockBoardGameGeekService.Object, _mapper);

            var result = await sut.Handle(request, CancellationToken.None);

            _mockBoardGameGeekService.Verify(r => r.SearchBoardGames(query), Times.Once);
        }

        [Test]
        public async Task IncludeAllSearchResultsInOutput()
        {
            var searchResults = new List<BggSearchResultItem>()
            {
                new BggSearchResultItem
                {
                    Id = 266192,
                    _name = new BggSearchResultItem.NameElement { Value = "Wingspan" },
                    _year = new BggSearchResultItem.YearElement { Value = "2019" }
                },
                new BggSearchResultItem
                {
                    Id = 290448,
                    _name = new BggSearchResultItem.NameElement
                    {
                        Value = "Wingspan: European Expansion"
                    },
                    _year = new BggSearchResultItem.YearElement { Value = "2019" }
                },
                new BggSearchResultItem
                {
                    Id = 300580,
                    _name = new BggSearchResultItem.NameElement
                    {
                        Value = "Wingspan: Oceania Expansion"
                    },
                    _year = new BggSearchResultItem.YearElement { Value = "2020" }
                }
            };

            string query = "wingspan";
            _mockBoardGameGeekService
                .Setup(s => s.SearchBoardGames(query))
                .ReturnsAsync(searchResults);

            var request = new SearchBoardGamesQuery { Name = query };

            var sut = new SearchBoardGamesQueryHandler(_mockBoardGameGeekService.Object, _mapper);

            var result = await sut.Handle(request, CancellationToken.None);

            searchResults.ForEach(inputItem =>
            {
                Assert.That(
                    result,
                    Has.One.Matches<BoardGameSearchResultDto>(outputItem =>
                        outputItem.BoardGameGeekId == inputItem.Id
                        && outputItem.Name == inputItem.Name
                        && outputItem.Year == inputItem.Year
                    )
                );
            });
        }
    }
}
