using AutoMapper;
using Borkfolio.Application.Contracts.Persistence;
using Borkfolio.Application.Features.BoardGames.Queries.GetSuggestions;
using Borkfolio.Application.Profiles;
using Borkfolio.Application.UnitTests.Mocks;
using Borkfolio.Domain.Entities;
using Moq;

namespace Borkfolio.Application.UnitTests.BoardGames.Queries
{
    public class GetSuggestionsShould
    {
        private readonly IMapper _mapper;

        private Mock<IAsyncRepository<Suggestion>> _mockSuggestionsRepository;

        public GetSuggestionsShould()
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
            _mockSuggestionsRepository = MockRepositoryFactory.GetMockRepository<
                IAsyncRepository<Suggestion>,
                Suggestion
            >();
        }

        [Test]
        public async Task GetSuggestionsFromRepository()
        {
            var request = new GetSuggestionsQuery();

            var sut = new GetSuggestionsQueryHandler(_mockSuggestionsRepository.Object, _mapper);

            var result = await sut.Handle(request, CancellationToken.None);

            _mockSuggestionsRepository.Verify(r => r.ListAllAsync(), Times.Once);
        }

        [Test]
        public async Task ContainAllRepositoryItemsInResult()
        {
            var suggestion = new Suggestion
            {
                BoardGame = new BoardGame
                {
                    BoardGameGeekId = 1,
                    Name = "Example Name 1",
                    Year = 2024
                },
            };

            await _mockSuggestionsRepository.Object.AddAsync(suggestion);

            var request = new GetSuggestionsQuery();

            var sut = new GetSuggestionsQueryHandler(_mockSuggestionsRepository.Object, _mapper);

            var result = await sut.Handle(request, CancellationToken.None);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null.And.Exactly(1).Items);
                Assert.That(
                    result,
                    Is.Not.Null.And.One.Matches<GetSuggestionsDto>(s =>
                        s.BoardGameGeekId == suggestion.BoardGame.BoardGameGeekId
                        && s.Year == suggestion.BoardGame.Year
                        && s.Name == suggestion.BoardGame.Name
                    )
                );
            });
        }
    }
}
